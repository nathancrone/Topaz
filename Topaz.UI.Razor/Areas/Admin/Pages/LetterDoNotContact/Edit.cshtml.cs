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

namespace Topaz.UI.Razor.Areas.Admin.Pages.LetterDoNotContact
{
    public class EditModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public EditModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId");
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

            _context.Attach(DoNotContactLetter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoNotContactLetterExists(DoNotContactLetter.DoNotContactLetterId))
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

        private bool DoNotContactLetterExists(int id)
        {
            return _context.DoNotContactLetters.Any(e => e.DoNotContactLetterId == id);
        }
    }
}
