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
        public readonly RoleManager<AppRole> _roleManager;

        [BindProperty]
        public AppUser AppUser { get; set; }

        [BindProperty]
        public string[] selectedRole { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public List<AppRole> Roles { get; set; }

        public CreateModel(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult OnGet()
        {
            Roles = _roleManager.Roles.ToList();
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

            var result = await _userManager.CreateAsync(AppUser, Password);

            if (result.Succeeded && selectedRole.Length != 0)
            {
                await _userManager.AddToRolesAsync(AppUser, selectedRole);
            }

            return RedirectToPage("./Index");
        }
    }
}
