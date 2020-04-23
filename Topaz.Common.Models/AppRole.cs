using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }
        public AppRole(string name) : base(name) { }
        public string Description { get; set; }
    }
}