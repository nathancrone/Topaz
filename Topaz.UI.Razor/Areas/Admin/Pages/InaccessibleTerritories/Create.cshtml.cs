using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.InaccessibleTerritories
{
    public class CreateModel : FormModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public CreateModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateStreetTerritory(_context);
            return Page();
        }

        [BindProperty]
        public InaccessibleTerritory InaccessibleTerritory { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateStreetTerritory(_context);
                return Page();
            }
            _context.InaccessibleTerritories.Add(InaccessibleTerritory);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
