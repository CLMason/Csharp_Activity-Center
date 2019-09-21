using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ActivityCenterI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace ActivityCenterI.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
     
        // here we can "inject" our context service into the constructor
        public static PasswordHasher<User> RegisterHasher = new PasswordHasher<User>();
        public static PasswordHasher<LoginUser> LoginHasher = new PasswordHasher<LoginUser>();
        public HomeController(MyContext context)
        {
            dbContext = context;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        // ==========================Register======================
        [HttpPost("register")]
        public IActionResult Register (User user)
        {
            if(dbContext.Users.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "Email already in use!");
                return View ("Index");
            }
            if(ModelState.IsValid)
            {
                var hasher = new PasswordHasher<User>();
                user.Password = hasher.HashPassword(user, user.Password);
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserId", user.UserId);
                return Redirect("/home");
            }
            return View ("Index");
        }
         // =======================Login==========================
        [HttpPost("login")]
        public IActionResult Login(LoginUser existingU)
        {
            User userInDb = dbContext.Users.FirstOrDefault(u => u.Email == existingU.LoginEmail);
            if(userInDb == null)
            {
                ModelState.AddModelError("LoginEmail", "Email not found! Please register.");
            } 
            else
            {
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(existingU, userInDb.Password, existingU.LoginPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Incorrect Password!");
                }
            }
            if(!ModelState.IsValid)
            {
                return View("Index");
            }
            
            HttpContext.Session.SetInt32("UserId", userInDb.UserId);
            return Redirect("/home");
        }
        // ===============Dashboard================
        [HttpGet("home")]
        public IActionResult Home()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId"); 
            if(UserId == null) //if UserId is not in session then redirect to register/login page
            {
                return Redirect("/");
            }
            List<Party> Parties = dbContext.Parties
                .Include(p => p.Planner)
                .Include(p => p.AttendingUsers)
                .OrderBy(p => p.PartyDate).ToList();
            ViewBag.Parties = Parties;
            ViewBag.UserId = UserId;
            User query = dbContext.Users
                       .Where(u => u.UserId == UserId)
                       .FirstOrDefault<User>();
            ViewBag.User= query;
            Console.WriteLine(query);
            return View("Home");
        }
        // =================Show New Acitivity Form==================
        [HttpGet("new")]
        public IActionResult AcitivityForm()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId==null)
            {
                return Redirect("/");
            }
            return View("Create");
        }
        // ================Create an Acitivity======================
        [HttpPost("create")]
        public IActionResult CreateActivity(Party a)
        {
            int? UserId=HttpContext.Session.GetInt32("UserId");
            if(UserId==null)
            {
                return Redirect("/");
            }
            if(ModelState.IsValid)
            {
                a.PlannerId=(int) UserId;
                dbContext.Parties.Add(a);
                dbContext.SaveChanges();
                return Redirect($"activity/{a.PartyId}");
            }
            else
            {
                return View("Create", a);

            }
        }
        // ===============Show Activity Details======================
        [HttpGet("/activity/{PartyId}")]
        public IActionResult Show(int PartyId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if(UserId==null)
            {
                return Redirect("/");
            }                
            Party p = dbContext.Parties
            .Include(pv => pv.Planner)
            .Include(pv => pv.AttendingUsers)
            .ThenInclude(pv => pv.Joiner)
            .FirstOrDefault(pv => pv.PartyId == PartyId);
            ViewBag.Joins=p.AttendingUsers;
            return View(p);
        }
        // =========================Join Activity========================
        [HttpGet("join/{PartyId}")]
            public IActionResult Join(int PartyId)
            {
                int? UserId=HttpContext.Session.GetInt32("UserId");
                if(UserId==null)
                {
                    return Redirect("/");
                }
                Join a = new Join()
                {
                    PartyId= PartyId,
                    UserId = (int) UserId
                };
                dbContext.Joins.Add(a);
                dbContext.SaveChanges();
                return Redirect("/home");
            }
        // ====================Delete an activity========================
        [HttpGet("cancel/{PartyId}")]
        
        public IActionResult cancel(int PartyId)
        {   
            int? UserId=HttpContext.Session.GetInt32("UserId");
            Party p1= dbContext.Parties.FirstOrDefault(w=>w.PartyId==PartyId);
            dbContext.Parties.Remove(p1);
            dbContext.SaveChanges();
            return Redirect("/home");
        }
        // ===================Leave an Activity============================
        [HttpGet("leave/{PartyId}")]
        
        public IActionResult leave(int PartyId)
        {   
            int? UserId=HttpContext.Session.GetInt32("UserId");
            Join jj = dbContext.Joins.Where(g=>g.PartyId==PartyId)
                .FirstOrDefault(g=>g.UserId==(int)UserId);
            dbContext.Joins.Remove(jj);
            dbContext.SaveChanges();
            return Redirect("/home");
        }
         // =======================LOGOUT=================================
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            return Redirect("/");
        }
        

    }
}