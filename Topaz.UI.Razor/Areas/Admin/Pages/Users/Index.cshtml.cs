using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly Topaz.Data.AuthDbContext _context;

        public IndexModel(Topaz.Data.AuthDbContext context)
        {
            _context = context;
        }

        public IList<AppUser> AppUser { get;set; }

        public async Task OnGetAsync()
        {
            AppUser = await _context.Users.ToListAsync();
        }
    }
}
