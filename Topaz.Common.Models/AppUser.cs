using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser() : base() { }
        public AppUser(string userName) : base(userName) { }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? PublisherId { get; set; }
    }
}