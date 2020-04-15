using System;
using System.Linq;
using System.Collections.Generic;
using Topaz.Data;
using Topaz.Common.Models;
using Topaz.UI.MigrationConsole.Legacy;
using Topaz.UI.MigrationConsole.Legacy.Models;
using Microsoft.EntityFrameworkCore;

namespace Topaz.UI.MigrationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            List<LegacyTerritory> legacyTerritories = new List<LegacyTerritory>();

            //get the legacy data out of the database
            using (var legacyDb = new LegacyDbContext())
            {
                legacyTerritories = legacyDb.LegacyTerritories.Include(a => a.LedgerEntries).ThenInclude(a => a.User).ToList();
            }

            //distinct list of legacy users
            List<LegacyUser> legacyUsers = legacyTerritories.SelectMany(a => a.LedgerEntries.Select(b => b.User)).Distinct().ToList();

            using (var db = new TopazDbContext())
            {
                //create the users
                foreach (var u in legacyUsers.OrderBy(a => a.LastName))
                {
                    db.Add(new Publisher { UserId = u.UserId, FirstName = u.FirstName, LastName = u.LastName });
                    db.SaveChanges();
                }

                //create the street territories
                foreach (var t in legacyTerritories.OrderBy(a => a.TerritoryCode))
                {
                    var street = new StreetTerritory { TerritoryCode = t.TerritoryCode, InActive = t.InActive };
                    db.Add(street);
                    db.SaveChanges();
                    foreach (var entry in t.LedgerEntries.OrderBy(x => x.CheckOutDate))
                    {
                        street.Activity.Add(new TerritoryActivity
                        {
                            PublisherId = db.Publishers.FirstOrDefault(x => x.UserId == entry.UserId).PublisherId,
                            CheckOutDate = entry.CheckOutDate,
                            CheckInDate = entry.CheckInDate
                        });
                        db.SaveChanges();
                    }
                }

            }

        }
    }
}
