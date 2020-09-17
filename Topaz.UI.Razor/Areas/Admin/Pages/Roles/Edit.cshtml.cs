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

namespace Topaz.UI.Razor.Areas.Admin.Pages.Roles
{
    public class EditModel : PageModel
    {
        public readonly RoleManager<AppRole> _roleManager;

        [BindProperty]
        public AppRole AppRole { get; set; }

        public EditModel(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppRole = await _roleManager.FindByIdAsync(id);

            if (AppRole == null)
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

            var appRoleToUpdate = await _roleManager.FindByIdAsync(AppRole.Id);
            appRoleToUpdate.Description = AppRole.Description;
            appRoleToUpdate.Name = AppRole.Name;

            await _roleManager.UpdateAsync(appRoleToUpdate);

            return RedirectToPage("./Index");
        }
    }
}
