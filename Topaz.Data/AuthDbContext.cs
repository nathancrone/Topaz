using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data.Configuration;

namespace Topaz.Data
{
    public class AuthDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfig());
            modelBuilder.ApplyConfiguration(new AppRoleConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
