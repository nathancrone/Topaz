using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}