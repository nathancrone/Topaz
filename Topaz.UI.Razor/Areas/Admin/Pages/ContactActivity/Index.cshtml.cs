using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.ContactActivity
{
    public class IndexModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public IndexModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public IList<InaccessibleContactActivity> InaccessibleContactActivity { get; set; }

        public async Task OnGetAsync()
        {
            InaccessibleContactActivity = await _context.InaccessibleContactActivities
                .Include(i => i.Contact)
                .Include(i => i.ContactActivityType)
                .Include(i => i.PhoneResponseType).ToListAsync();
        }
    }
}
