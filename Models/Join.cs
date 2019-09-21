using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ActivityCenterI.Models
{
    public class Join //tell us who is going to the activity
    {
        [Key]
        public int JoinId{get;set;}
        public int UserId {get;set;}//user who is going

        public int PartyId {get;set;}
        
        public User Joiner {get;set;} //User joining the activity

        public Party JoinedParty {get;set;} //the acitivity a user wants to see
    }
}