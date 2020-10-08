using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.LetterDoNotContact
{
    public class IndexModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public IndexModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public IList<DoNotContactLetter> DoNotContactLetter { get;set; }

        public async Task OnGetAsync()
        {
            DoNotContactLetter = await _context.DoNotContactLetters
                .Include(d => d.Publisher)
                .Include(d => d.Territory).ToListAsync();
        }
    }
}
