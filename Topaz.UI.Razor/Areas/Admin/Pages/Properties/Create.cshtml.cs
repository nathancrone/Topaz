using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.Properties
{
    public class CreateModel : FormModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public CreateModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int territoryId)
        {
            PopulateTerritory(_context, territoryId);
            return Page();
        }

        [BindProperty]
        public InaccessibleProperty InaccessibleProperty { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateTerritory(_context);
                return Page();
            }

            _context.InaccessibleProperties.Add(InaccessibleProperty);
            await _context.SaveChangesAsync();

            return RedirectToPage("../InaccessibleTerritories/Details", new { id = InaccessibleProperty.TerritoryId });
        }
    }
}
