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

        [BindProperty]
        public AppUser AppUser { get; set; }

        public EditModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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

            await _userManager.UpdateAsync(appUserToUpdate);

            return RedirectToPage("./Index");
        }
    }
}
