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

namespace Topaz.UI.Razor.Pages.InaccessibleTerritories
{
    public class FormModel : PageModel
    {
        public SelectList SelectListStreetTerritory { get; set; }

        public void PopulateStreetTerritory(TopazDbContext context, object selectedStreetTerritory = null)
        {
            var streetTerritoriesQuery = context.StreetTerritories.OrderBy(x => x.TerritoryCode).Select(x => x);
            SelectListStreetTerritory = new SelectList(streetTerritoriesQuery.AsNoTracking(), "TerritoryId", "TerritoryCode", selectedStreetTerritory);
        }
    }
}
