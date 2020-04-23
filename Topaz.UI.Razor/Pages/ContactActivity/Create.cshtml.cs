using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Pages.ContactActivity
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
            ViewData["InaccessibleContactId"] = new SelectList(_context.InaccessibleContacts, "InaccessibleContactId", "InaccessibleContactId");
            ViewData["ContactActivityTypeId"] = new SelectList(_context.ContactActivityTypes, "ContactActivityTypeId", "ContactActivityTypeId");
            ViewData["PhoneResponseTypeId"] = new SelectList(_context.PhoneResponseTypes, "PhoneResponseTypeId", "PhoneResponseTypeId");
            return Page();
        }

        [BindProperty]
        public InaccessibleContactActivity InaccessibleContactActivity { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.InaccessibleContactActivities.Add(InaccessibleContactActivity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
