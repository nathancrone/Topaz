using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;
using Microsoft.AspNetCore.Identity;

namespace Topaz.UI.Razor.Areas.Admin.Pages.Users
{
    public class DeleteModel : PageModel
    {
        public readonly UserManager<AppUser> _userManager;

        [BindProperty]
        public AppUser AppUser { get; set; }

        public DeleteModel(UserManager<AppUser> userManager)
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

        public async Task<IActionResult> OnPostAsync(string id)
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

            await _userManager.DeleteAsync(AppUser);

            return RedirectToPage("./Index");
        }
    }
}
