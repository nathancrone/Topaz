using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Pages.Properties
{
    public class DetailsModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public DetailsModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public InaccessibleProperty InaccessibleProperty { get; set; }
        public InaccessibleContactList InaccessibleContactList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InaccessibleProperty = await _context.InaccessibleProperties
                .Include(i => i.Territory)
                .ThenInclude(x => x.StreetTerritory)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InaccessiblePropertyId == id);

            //InaccessibleContactList = InaccessibleProperty.ContactLists.FirstOrDefault(x => x.InaccessibleContactListId == InaccessibleProperty.CurrentContactListId);

            if (InaccessibleProperty == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
