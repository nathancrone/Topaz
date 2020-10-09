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

namespace Topaz.UI.Razor.Areas.Admin.Pages.LetterDoNotContact
{
    public class FormModel : PageModel
    {
        public SelectList SelectListStreetTerritory { get; set; }
        public SelectList SelectListPublisher { get; set; }

        public void PopulateSelectLists(TopazDbContext context, object selectedStreetTerritory = null, object selectedPublisher = null)
        {
            var streetTerritoriesQuery = context.StreetTerritories.OrderBy(x => x.TerritoryCode).Select(x => x);
            SelectListStreetTerritory = new SelectList(streetTerritoriesQuery.AsNoTracking(), "TerritoryId", "TerritoryCode", selectedStreetTerritory);

            var publishersQuery = context.Publishers.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).Select(x => new { Value = x.PublisherId, Text = $"{x.LastName}, {x.FirstName}" });
            SelectListPublisher = new SelectList(publishersQuery.AsNoTracking(), "Value", "Text", selectedPublisher);
        }
    }
}
