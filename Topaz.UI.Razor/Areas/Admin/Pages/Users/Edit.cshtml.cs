using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;
using Microsoft.AspNetCore.Identity;

namespace Topaz.UI.Razor.Areas.Admin.Pages.Users
{
    public class EditModel : PageModel
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly RoleManager<AppRole> _roleManager;

        [BindProperty]
        public AppUser AppUser { get; set; }

        [BindProperty]
        public string[] selectedRole { get; set; }

        public List<AppRole> Roles { get; set; }
        public IList<string> UserRoles { get; set; }

        public EditModel(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppUser = await _userManager.FindByIdAsync(id);

            if (AppUser == null)
            {
                return NotFound();
            }

            Roles = _roleManager.Roles.ToList();
            UserRoles = await _userManager.GetRolesAsync(AppUser);


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var appUserToUpdate = await _userManager.FindByIdAsync(AppUser.Id);
            appUserToUpdate.UserName = AppUser.UserName;
            appUserToUpdate.FirstName = AppUser.FirstName;
            appUserToUpdate.LastName = AppUser.LastName;
            appUserToUpdate.Email = AppUser.Email;

            var result = await _userManager.UpdateAsync(appUserToUpdate);

            if (result.Succeeded)
            {
                UserRoles = await _userManager.GetRolesAsync(AppUser);
                if (selectedRole.Except(UserRoles).Count() != 0)
                {
                    await _userManager.AddToRolesAsync(appUserToUpdate, selectedRole.Except(UserRoles).ToList<string>());
                }
                if (UserRoles.Except(selectedRole).Count() != 0)
                {
                    await _userManager.RemoveFromRolesAsync(appUserToUpdate, UserRoles.Except(selectedRole).ToList<string>());
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
