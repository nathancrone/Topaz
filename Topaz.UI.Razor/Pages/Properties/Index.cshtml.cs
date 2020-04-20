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
    public class IndexModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public IndexModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public IList<InaccessibleProperty> InaccessibleProperty { get; set; }

        public async Task OnGetAsync()
        {
            InaccessibleProperty = await _context.InaccessibleProperties
                .Include(i => i.Territory)
                .ThenInclude(x => x.StreetTerritory)
                .ToListAsync();
        }
    }
}
