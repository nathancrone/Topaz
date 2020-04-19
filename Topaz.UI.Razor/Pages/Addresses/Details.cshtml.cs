using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Pages.Addresses
{
    public class DetailsModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public DetailsModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public InaccessibleAddress InaccessibleAddress { get; set; }
        public InaccessibleContactList InaccessibleContactList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InaccessibleAddress = await _context.InaccessibleAddresses
                .Include(i => i.Territory)
                .Include(x => x.ContactLists)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InaccessibleAddressId == id);

            InaccessibleContactList = InaccessibleAddress.ContactLists.FirstOrDefault(x => x.InaccessibleContactListId == InaccessibleAddress.CurrentContactListId);

            if (InaccessibleAddress == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
