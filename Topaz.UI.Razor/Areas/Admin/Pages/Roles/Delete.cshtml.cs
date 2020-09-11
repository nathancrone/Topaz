using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.Roles
{
    public class DeleteModel : PageModel
    {
        private readonly Topaz.Data.AuthDbContext _context;

        public DeleteModel(Topaz.Data.AuthDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AppRole AppRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppRole = await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);

            if (AppRole == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppRole = await _context.Roles.FindAsync(id);

            if (AppRole != null)
            {
                _context.Roles.Remove(AppRole);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
