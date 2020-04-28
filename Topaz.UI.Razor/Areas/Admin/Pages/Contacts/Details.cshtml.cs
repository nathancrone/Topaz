using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.Contacts
{
    public class DetailsModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public DetailsModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
