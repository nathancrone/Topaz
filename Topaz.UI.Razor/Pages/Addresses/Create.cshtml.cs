using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Pages.Addresses
{
    public class CreateModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public CreateModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["TerritoryId"] = new SelectList(_context.InaccessibleTerritories, "TerritoryId", "Discriminator");
            return Page();
        }

        [BindProperty]
        public InaccessibleAddress InaccessibleAddress { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.InaccessibleAddresses.Add(InaccessibleAddress);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
