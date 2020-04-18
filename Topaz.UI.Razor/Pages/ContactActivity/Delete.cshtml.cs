using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Pages.ContactActivity
{
    public class DeleteModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public DeleteModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InaccessibleContactActivity InaccessibleContactActivity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InaccessibleContactActivity = await _context.InaccessibleContactActivities
                .Include(i => i.Contact)
                .Include(i => i.ContactActivityType)
                .Include(i => i.PhoneResponseType).FirstOrDefaultAsync(m => m.InaccessibleContactActivityId == id);

            if (InaccessibleContactActivity == null)
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

            InaccessibleContactActivity = await _context.InaccessibleContactActivities.FindAsync(id);

            if (InaccessibleContactActivity != null)
            {
                _context.InaccessibleContactActivities.Remove(InaccessibleContactActivity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
