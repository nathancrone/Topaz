using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.PhoneDoNotContact
{
    public class DetailsModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;

        public DetailsModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public DoNotContactPhone DoNotContactPhone { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DoNotContactPhone = await _context.DoNotContactPhones
                .Include(d => d.Publisher).FirstOrDefaultAsync(m => m.DoNotContactPhoneId == id);

            if (DoNotContactPhone == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
