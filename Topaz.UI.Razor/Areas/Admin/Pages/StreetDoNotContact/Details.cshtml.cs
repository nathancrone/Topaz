using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.StreetDoNotContact
{
    public class DetailsModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public DetailsModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public DoNotContactStreet DoNotContactStreet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DoNotContactStreet = await _context.DoNotContactStreets
                .Include(d => d.Publisher)
                .Include(d => d.Territory).FirstOrDefaultAsync(m => m.DoNotContactStreetId == id);

            if (DoNotContactStreet == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
