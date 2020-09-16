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
    public class IndexModel : PageModel
    {
        public readonly UserManager<AppUser> _userManager;
        public IList<AppUser> AppUser { get; set; }

        public IndexModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            AppUser = await _userManager.Users.ToListAsync();
        }
    }
}
