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

namespace Topaz.UI.Razor.Pages.Properties
{
    public class FormModel : PageModel
    {
        public SelectList SelectListTerritory { get; set; }

        public void PopulateTerritory(TopazDbContext context, object selectedTerritory = null)
        {
            var territoriesQuery = context.InaccessibleTerritories
                .Include(x => x.StreetTerritory)
                .OrderBy(x => x.StreetTerritory.TerritoryCode)
                .ThenBy(x => x.TerritoryCode)
                .Select(x => new
                {
                    Value = x.TerritoryId,
                    Text = $"{x.StreetTerritory.TerritoryCode} / {x.TerritoryCode}"
                });
            SelectListTerritory = new SelectList(territoriesQuery.AsNoTracking(), "Value", "Text", selectedTerritory);
        }
    }
}
