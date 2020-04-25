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

        public UserCreationService(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task CreateUser()
        {
            var user = new AppUser { UserName = "nathan.crone@gmail.com", Email = "nathan.crone@gmail.com" };
            var result = await this._userManager.CreateAsync(user, "Test@123");

            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
            else
            {
                Console.WriteLine("Done.");
            }
        }
    }
}