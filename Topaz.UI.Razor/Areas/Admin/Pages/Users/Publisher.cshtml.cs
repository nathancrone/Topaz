using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;
using Microsoft.AspNetCore.Identity;

namespace Topaz.UI.Razor.Areas.Admin.Pages.Users
{
    public class PublisherModel : PageModel
    {
        private readonly TopazDbContext _context;
        public readonly UserManager<AppUser> _userManager;

        public IEnumerable<SelectListItem> Publishers { get; set; }

        [BindProperty]
        public AppUser AppUser { get; set; }

        public PublisherModel(TopazDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppUser = await _userManager.FindByIdAsync(id);

            if (AppUser == null)
            {
                return NotFound();
            }

            Publishers = _context.Publishers
                .Where(x => !x.IsHidden)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Select(x => new SelectListItem { Value = x.PublisherId.ToString(), Text = $"{x.LastName}, {x.FirstName}", Selected = AppUser.PublisherId == x.PublisherId })
                .AsNoTracking()
                .ToList();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var appUserToUpdate = await _userManager.FindByIdAsync(AppUser.Id);
            appUserToUpdate.PublisherId = AppUser.PublisherId;

            var result = await _userManager.UpdateAsync(appUserToUpdate);

            return RedirectToPage("./Index");
        }
    }
}
