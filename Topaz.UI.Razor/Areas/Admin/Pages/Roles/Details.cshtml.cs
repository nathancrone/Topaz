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

namespace Topaz.UI.Razor.Areas.Admin.Pages.Roles
{
    public class DetailsModel : PageModel
    {
        public readonly RoleManager<AppRole> _roleManager;

        [BindProperty]
        public AppRole AppRole { get; set; }

        public DetailsModel(RoleManager<AppRole> roleManager)
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
    }
}
