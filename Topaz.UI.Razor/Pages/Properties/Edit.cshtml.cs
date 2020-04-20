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

namespace Topaz.UI.Razor.Pages.Properties
{
    public class EditModel : FormModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public EditModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InaccessibleProperty InaccessibleProperty { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InaccessibleProperty = await _context.InaccessibleProperties
                .Include(i => i.Territory).FirstOrDefaultAsync(m => m.InaccessiblePropertyId == id);

            if (InaccessibleProperty == null)
            {
                return NotFound();
            }
            PopulateTerritory(_context);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateTerritory(_context);
                return Page();
            }

            _context.Attach(InaccessibleProperty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InaccessiblePropertyExists(InaccessibleProperty.InaccessiblePropertyId))
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

        private bool InaccessiblePropertyExists(int id)
        {
            return _context.InaccessibleProperties.Any(e => e.InaccessiblePropertyId == id);
        }
    }
}
