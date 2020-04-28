using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.Contacts
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
            ViewData["InaccessibleContactListId"] = new SelectList(_context.InaccessibleContactLists, "InaccessibleContactListId", "InaccessibleContactListId");
            ViewData["PhoneTypeId"] = new SelectList(_context.PhoneType, "PhoneTypeId", "PhoneTypeId");
            return Page();
        }

        [BindProperty]
        public InaccessibleContact InaccessibleContact { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.InaccessibleContacts.Add(InaccessibleContact);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
