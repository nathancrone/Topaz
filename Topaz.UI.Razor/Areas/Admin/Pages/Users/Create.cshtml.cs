using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Topaz.Common.Models;
using Topaz.Data;
using Microsoft.AspNetCore.Identity;

namespace Topaz.UI.Razor.Areas.Admin.Pages.Users
{
    public class CreateModel : PageModel
    {
        public readonly UserManager<AppUser> _userManager;

        [BindProperty]
        public AppUser AppUser { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public CreateModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
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

            await _userManager.CreateAsync(AppUser, Password);

            return RedirectToPage("./Index");
        }
    }
}
