using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Enums;
using Topaz.Common.Models;
using Topaz.Data;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            if (InaccessibleProperty.CurrentContactListId != null)
            {
                InaccessibleContactList = await _context.InaccessibleContactLists
                    .Include(x => x.Contacts)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.InaccessibleContactListId == InaccessibleProperty.CurrentContactListId);
            }

            if (InaccessibleProperty == null)
            {
                return NotFound();
            }
            return Page();
        }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serializer = new JsonSerializer();
            using (var memoryStream = new MemoryStream())
            {
                await Upload.CopyToAsync(memoryStream);
                using (var streamReader = new StreamReader(memoryStream))
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    await memoryStream.FlushAsync();
                    memoryStream.Position = 0;

                    var uploadJson = (JArray)JToken.ReadFrom(jsonTextReader);

                    //validate the json

                    var list = new InaccessibleContactList()
                    {
                        InaccessiblePropertyId = id ?? 0,
                        CreateDate = System.DateTime.UtcNow
                    };

                    foreach (JObject objContact in uploadJson)
                    {
                        PhoneTypeEnum phoneType;

                        switch ((string)objContact["PhoneType"])
                        {
                            case "C":
                                phoneType = PhoneTypeEnum.Mobile;
                                break;
                            case "L":
                                phoneType = PhoneTypeEnum.Landline;
                                break;
                            case "V":
                                phoneType = PhoneTypeEnum.Voip;
                                break;
                            default:
                                phoneType = PhoneTypeEnum.Landline;
                                break;
                        }

                        list.Contacts.Add(
                            new InaccessibleContact()
                            {
                                FirstName = (string)objContact["FirstName"],
                                LastName = (string)objContact["LastName"],
                                MiddleInitial = (string)objContact["MiddleInitial"],
                                Age = (int)objContact["Age"],
                                MailingAddress1 = (string)objContact["MailingAddress1"],
                                MailingAddress2 = (string)objContact["MailingAddress2"],
                                PostalCode = (string)objContact["PostalCode"],
                                PhoneNumber = (string)objContact["PhoneNumber"],
                                PhoneTypeId = (int)phoneType
                            }
                        );
                    }

                    await _context.InaccessibleContactLists.AddAsync(list);
                    await _context.SaveChangesAsync();
                }
            }

            InaccessibleProperty = await _context.InaccessibleProperties
                .Include(i => i.Territory)
                .ThenInclude(x => x.StreetTerritory)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.InaccessiblePropertyId == id);

            if (InaccessibleProperty.CurrentContactListId != null)
            {
                InaccessibleContactList = await _context.InaccessibleContactLists
                    .Include(x => x.Contacts)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.InaccessibleContactListId == InaccessibleProperty.CurrentContactListId);
            }

            if (InaccessibleProperty == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
