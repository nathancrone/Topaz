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

namespace Topaz.UI.Razor.Pages.InaccessibleTerritories
{
    public class EditModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public EditModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InaccessibleTerritory InaccessibleTerritory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InaccessibleTerritory = await _context.InaccessibleTerritories.FirstOrDefaultAsync(m => m.TerritoryId == id);

            if (InaccessibleTerritory == null)
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

            _context.Attach(InaccessibleTerritory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InaccessibleTerritoryExists(InaccessibleTerritory.TerritoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InaccessibleTerritoryExists(int id)
        {
            return _context.InaccessibleTerritories.Any(e => e.TerritoryId == id);
        }
    }
}
