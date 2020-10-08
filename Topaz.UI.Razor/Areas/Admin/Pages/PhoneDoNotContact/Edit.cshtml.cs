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

namespace Topaz.UI.Razor.Areas.Admin.Pages.PhoneDoNotContact
{
    public class EditModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public EditModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DoNotContactPhone DoNotContactPhone { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DoNotContactPhone = await _context.DoNotContactPhones
                .Include(d => d.Publisher).FirstOrDefaultAsync(m => m.DoNotContactPhoneId == id);

            if (DoNotContactPhone == null)
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

            _context.Attach(DoNotContactPhone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoNotContactPhoneExists(DoNotContactPhone.DoNotContactPhoneId))
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

        private bool DoNotContactPhoneExists(int id)
        {
            return _context.DoNotContactPhones.Any(e => e.DoNotContactPhoneId == id);
        }
    }
}
