using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;
using System.Security.Claims;

namespace Topaz.UI.Razor.Pages.StreetActivity
{
    public class IndexModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public IndexModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public IList<Topaz.Common.Models.TerritoryActivity> CheckedOut { get; set; }

        public async Task OnGetAsync()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var UserId = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            CheckedOut = await _context.TerritoryActivities
                .Where(x => x.Publisher.UserId == UserId && x.CheckInDate == null && x.StreetTerritory != null)
                .Include(x => x.StreetTerritory)
                .Include(x => x.Publisher)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
