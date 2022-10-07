using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.ApartmentTerritories
{
    public class DeleteModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public DeleteModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ApartmentTerritory ApartmentTerritory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApartmentTerritory = await _context.ApartmentTerritories.Include(x => x.StreetTerritory).FirstOrDefaultAsync(m => m.TerritoryId == id);

            if (ApartmentTerritory == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApartmentTerritory = await _context.ApartmentTerritories.FindAsync(id);

            if (ApartmentTerritory != null)
            {
                _context.ApartmentTerritories.Remove(ApartmentTerritory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
