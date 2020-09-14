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

namespace Topaz.UI.Razor.Areas.Admin.Pages.Contacts
{
    public class EditModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public EditModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InaccessibleContact InaccessibleContact { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InaccessibleContact = await _context.InaccessibleContacts
                .Include(i => i.ContactList)
                .Include(i => i.PhoneType).FirstOrDefaultAsync(m => m.InaccessibleContactId == id);

            if (InaccessibleContact == null)
            {
                return NotFound();
            }
            ViewData["InaccessibleContactListId"] = new SelectList(_context.InaccessibleContactLists, "InaccessibleContactListId", "InaccessibleContactListId");
            ViewData["PhoneTypeId"] = new SelectList(_context.PhoneType, "PhoneTypeId", "PhoneTypeId");
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

            _context.Attach(InaccessibleContact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InaccessibleContactExists(InaccessibleContact.InaccessibleContactId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var list = await _context.InaccessibleContactLists.FirstOrDefaultAsync(x => x.InaccessibleContactListId == InaccessibleContact.InaccessibleContactListId);
            return RedirectToPage("../Properties/Details", new { id = list.InaccessiblePropertyId });
        }

        private bool InaccessibleContactExists(int id)
        {
            return _context.InaccessibleContacts.Any(e => e.InaccessibleContactId == id);
        }
    }
}
