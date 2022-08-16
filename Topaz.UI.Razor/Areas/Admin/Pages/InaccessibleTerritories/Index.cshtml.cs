using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data;

namespace Topaz.UI.Razor.Areas.Admin.Pages.InaccessibleTerritories
{
    public class IndexModel : PageModel
    {
        [FromQuery(Name = "showInactive")]
        public bool showInactive { get; set; }

        private readonly Topaz.Data.TopazDbContext _context;

        public IndexModel(Topaz.Data.TopazDbContext context)
        {
            _context = context;
        }

        public Dictionary<long, (DateTime? CheckInDate, DateTime? CreateDate)> DateLookup { get; set; }
        public IList<InaccessibleTerritory> InaccessibleTerritory { get; set; }

        public async Task OnGetAsync()
        {
            var sql = @"
                SELECT t2.TerritoryId, MAX(a2.CheckInDate) CheckInDate, cd.CreateDate CreateDate
                FROM Territories t2 
                    INNER JOIN TerritoryActivities a2 ON t2.TerritoryId = a2.TerritoryId
                    INNER JOIN (
                        SELECT t2_.TerritoryId,
                            MIN(l.CreateDate) CreateDate
                        FROM Territories t2_
                            INNER JOIN InaccessibleProperties p ON t2_.TerritoryId = p.TerritoryId
                            INNER JOIN InaccessibleContactLists l ON p.CurrentContactListId = l.InaccessibleContactListId
                        WHERE t2_.Discriminator = 'InaccessibleTerritory'
                        GROUP BY t2_.TerritoryId
                    ) cd ON t2.TerritoryId = cd.TerritoryId
                WHERE t2.InActive = 0 AND t2.Discriminator = 'InaccessibleTerritory' 
                GROUP BY t2.TerritoryId";


            var lookup = new Dictionary<long, (DateTime? CheckInDate, DateTime? CreateDate)>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                _context.Database.OpenConnection();
                using (var results = command.ExecuteReader())
                {
                    while (results.Read())
                    {

                        DateTime? checkInDate = null;
                        if (results["CheckInDate"] != System.DBNull.Value)
                        {
                            checkInDate = DateTime.Parse((string)results["CheckInDate"]);
                        }

                        DateTime? createDate = null;
                        if (results["CreateDate"] != System.DBNull.Value)
                        {
                            createDate = DateTime.Parse((string)results["CreateDate"]);
                        }

                        lookup.Add((long)results["TerritoryId"], (checkInDate, createDate));
                    }
                }
            }
            DateLookup = lookup;

            InaccessibleTerritory = await _context.InaccessibleTerritories
                .Where(x => showInactive || !x.InActive)
                .Include(x => x.StreetTerritory)
                .OrderBy(x => x.TerritoryCode)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
