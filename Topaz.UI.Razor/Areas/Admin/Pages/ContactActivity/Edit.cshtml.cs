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

namespace Topaz.UI.Razor.Areas.Admin.Pages.ContactActivity
{
    public class EditModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public EditModel(Topaz.Data.TopazDbContext context)
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
            ViewData["InaccessibleContactId"] = new SelectList(_context.InaccessibleContacts, "InaccessibleContactId", "InaccessibleContactId");
            ViewData["ContactActivityTypeId"] = new SelectList(_context.ContactActivityTypes, "ContactActivityTypeId", "ContactActivityTypeId");
            ViewData["PhoneResponseTypeId"] = new SelectList(_context.PhoneResponseTypes, "PhoneResponseTypeId", "PhoneResponseTypeId");
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

            _context.Attach(InaccessibleContactActivity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InaccessibleContactActivityExists(InaccessibleContactActivity.InaccessibleContactActivityId))
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

        private bool InaccessibleContactActivityExists(int id)
        {
            return _context.InaccessibleContactActivities.Any(e => e.InaccessibleContactActivityId == id);
        }
    }
}
