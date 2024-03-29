using System;
using System.Collections.Generic;
using BookStore.Data;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Models
{
    //use for adding additional columns to AspNetUsers
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        
        public ICollection<Books> Books { get; set; }
    }
}