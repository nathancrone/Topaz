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
            var territoriesQuery = context.InaccessibleTerritories.OrderBy(x => x.TerritoryCode).Select(x => x);
            SelectListTerritory = new SelectList(territoriesQuery.AsNoTracking(), "TerritoryId", "TerritoryCode", selectedTerritory);
        }
    }
}
