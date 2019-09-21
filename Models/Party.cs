using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace ActivityCenterI.Models
{
    public class Party
    {
        [Key]
        public int PartyId { get; set; }

        [Required(ErrorMessage = "Please re-enter your Activity")]
        public string PartyName { get; set; }



        [Required(ErrorMessage = "Please re-enter your Activity date")]
        [FutureDate]
        public DateTime PartyDate { get; set; }


        [Required(ErrorMessage = "Please re-enter your Time format")]
        public string PartyTime { get; set; }



        [Required(ErrorMessage = "Please re-enter your Duration")]
        public int Duration { get; set; }



        [Required(ErrorMessage = "Please re-enter your Descrption")] 
        public string Description { get; set; }


        public int PlannerId { get; set; }

        public User Planner { get; set; }

        public List<Join> AttendingUsers { get; set; }
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}

    }
}