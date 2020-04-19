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
    public class DetailsModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public DetailsModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public InaccessibleTerritory InaccessibleTerritory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InaccessibleTerritory = await _context.InaccessibleTerritories
                .Include(x => x.StreetTerritory)
                .Include(x => x.InaccessibleAddresses)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.TerritoryId == id);

            if (InaccessibleTerritory == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
