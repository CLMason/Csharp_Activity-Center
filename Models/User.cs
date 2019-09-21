using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace ActivityCenterI.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}
        [Required(ErrorMessage="A Name is required to register")]
        [MinLength(2, ErrorMessage="Your name must be at least 2 characters long")]
        public string Name {get;set;}


        [Required(ErrorMessage="An Email address is required to register!")]
        [EmailAddress]
        public string Email {get;set;}



        [Required(ErrorMessage="You need a password to register!")]
        [RegularExpression("^(?=.{8,}$)(?=.*?[A-Za-z])(?=.*?[0-9])(?=.*?[!@#$%^&*()]).*$",ErrorMessage="Password must contain at least 1 number, 1 letter, and 1 special character and 8 characters long.")]   
        [DataType(DataType.Password)]
        public string Password {get;set;}


        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}

        public List<Party> PlannedParties {get; set;}
        public List<Join> AttendingParties {get; set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}