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
    public class IndexModel : PageModel
    {
        public readonly RoleManager<AppRole> _roleManager;

        public IList<AppRole> AppRole { get; set; }

        public IndexModel(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task OnGetAsync()
        {
            AppRole = await _roleManager.Roles.ToListAsync();
        }
    }
}
