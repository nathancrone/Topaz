using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.BusinessTerritories
{
    public class DeleteModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public DeleteModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BusinessTerritory BusinessTerritory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BusinessTerritory = await _context.BusinessTerritories.FirstOrDefaultAsync(m => m.TerritoryId == id);

            if (BusinessTerritory == null)
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

            BusinessTerritory = await _context.BusinessTerritories.FindAsync(id);

            if (BusinessTerritory != null)
            {
                _context.BusinessTerritories.Remove(BusinessTerritory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
