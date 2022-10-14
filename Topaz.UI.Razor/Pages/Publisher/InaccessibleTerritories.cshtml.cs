using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Topaz.Common.Models.Extensions;

namespace Topaz.UI.Razor.Pages.Publisher
{
    public class InaccessibleTerritoriesModel : PageModel
    {
        private readonly Topaz.Data.TopazDbContext _context;
        private readonly ILogger<InaccessibleTerritoriesModel> _logger;
        private (string name, int? columnIndex, bool columnrequired)[] columnInformation = new (string name, int? columnIndex, bool columnrequired)[] {
            ("FirstName", null, true),
            ("LastName", null, true),
            ("MiddleInitial", null, false),
            ("Age", null, false),
            ("PhoneNumber", null, true),
            ("PhoneType", null, true),
            ("MailingAddress1", null, true),
            ("MailingAddress2", null, false),
            ("PostalCode", null, true)
        };

        [BindProperty]
        public int[] InaccessibleContactIds { get; set; } = new int[0];

        public InaccessibleTerritoriesModel(Topaz.Data.TopazDbContext context, ILogger<InaccessibleTerritoriesModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            var download = _context.InaccessibleContacts.Include(x => x.PhoneType).Where(x => InaccessibleContactIds.Contains(x.InaccessibleContactId));

            using (MemoryStream memoryStream1 = new MemoryStream())
            using (StreamWriter streamWriter1 = new StreamWriter(memoryStream1))
            {
                try
                {
                    streamWriter1.WriteLine(string.Join(",", columnInformation.Select(x => x.name).ToArray()));
                    foreach (var contact in download)
                    {
                        streamWriter1.WriteLine(contact.ToCsv());
                    }
                }
                catch
                {

                }
                finally
                {
                    streamWriter1.Flush();
                    streamWriter1.Close();
                }

                var fileName = $"contactdownload";

                Response.Headers.Add("Content-Disposition", $"inline; filename={fileName}.csv");
                return File(memoryStream1.ToArray(), "text/csv", $"{fileName}.csv");
            }
        }
    }
}
