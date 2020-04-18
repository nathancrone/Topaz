using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Pages.InaccessibleTerritories
{
    public class IndexModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public IndexModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public IList<InaccessibleTerritory> InaccessibleTerritory { get; set; }

        public async Task OnGetAsync()
        {
            InaccessibleTerritory = await _context.InaccessibleTerritories
                .Include(x => x.StreetTerritory)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
