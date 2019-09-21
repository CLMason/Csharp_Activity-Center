using Microsoft.EntityFrameworkCore;
using ActivityCenterI.Models;

namespace ActivityCenterI.Models {
    public class MyContext : DbContext {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get; set;}
        public DbSet<Party> Parties {get;set;}
        public DbSet<Join> Joins {get;set;}
    }
}
