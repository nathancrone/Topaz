using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.LetterDoNotContact
{
    public class DetailsModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public DetailsModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public DoNotContactLetter DoNotContactLetter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DoNotContactLetter = await _context.DoNotContactLetters
                .Include(d => d.Publisher)
                .Include(d => d.Territory).FirstOrDefaultAsync(m => m.DoNotContactLetterId == id);

            if (DoNotContactLetter == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
