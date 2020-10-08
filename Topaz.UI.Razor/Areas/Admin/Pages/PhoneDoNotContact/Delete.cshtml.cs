using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.PhoneDoNotContact
{
    public class DeleteModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public DeleteModel(Topaz.Data.TopazDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DoNotContactPhone = await _context.DoNotContactPhones.FindAsync(id);

            if (DoNotContactPhone != null)
            {
                _context.DoNotContactPhones.Remove(DoNotContactPhone);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
