using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Topaz.Common.Models;
using Topaz.UI.Consoles.UserConsole.Interfaces;

namespace Topaz.UI.Consoles.UserConsole.Services
{
    public class UserCreationService : IUserCreationService
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<AppRole> _roleManager;

        public UserCreationService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task CreateRole()
        {
            var roles = new List<AppRole>() {
                new AppRole { Name = "Administrator", Description = "Application admin role (gives the user all permissions)" },
                new AppRole { Name = "Publisher", Description = "Can check out territory, check in territory, and view territories checked out to them." }
            };

            foreach (var role in roles)
            {
                var result = await this._roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
                else
                {
                    Console.WriteLine("role created.");
                }
            }
        }

        public async Task CreateUser()
        {
            var users = new List<AppUser>() {
                new AppUser { UserName = "nathan.crone@gmail.com", Email = "nathan.crone@gmail.com" },
                new AppUser { UserName = "danashelyce123@gmail.com", Email = "danashelyce123@gmail.com" }
            };

            foreach (var user in users)
            {
                var result = await this._userManager.CreateAsync(user, "Test@123");

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, "Administrator");
                    Console.WriteLine("user created.");
                }
            }
        }
    }
}