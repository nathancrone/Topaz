using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Topaz.Common.Models;
using Topaz.Data;
using Topaz.UI.Consoles.MigrationConsole.Legacy;
using Topaz.UI.Consoles.MigrationConsole.Legacy.Models;

namespace Topaz.UI.Consoles.MigrationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create service collection and configure our services
            var services = ConfigureServices();
            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();
            // Kick off our actual code
            serviceProvider.GetService<ConsoleApp>().Run();
            Console.WriteLine("done");
            Console.ReadLine();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<LegacyDbContext, LegacyDbContext>();
            services.AddDbContext<LegacyDbContext>(options => options.UseSqlite("Data Source=JasperDB.db"));
            services.AddTransient<TopazDbContext, TopazDbContext>();
            services.AddDbContext<TopazDbContext>(options => options.UseSqlite("Data Source=TopazDb.db"));
            // IMPORTANT! Register our application entry point
            services.AddTransient<ConsoleApp>();
            return services;
        }
    }
    public class ConsoleApp
    {
        private TopazDbContext _db;
        private LegacyDbContext _legacyDb;
        public ConsoleApp(LegacyDbContext legacyDb, TopazDbContext db)
        {
            _legacyDb = legacyDb;
            _db = db;
        }

        // Application starting point
        public void Run()
        {
            List<LegacyTerritory> legacyTerritories = new List<LegacyTerritory>();

            //get the legacy data out of the database
            legacyTerritories = _legacyDb.LegacyTerritories.Include(a => a.LedgerEntries).ThenInclude(a => a.User).ToList();

            //distinct list of legacy users
            List<LegacyUser> legacyUsers = legacyTerritories.SelectMany(a => a.LedgerEntries.Select(b => b.User)).Distinct().ToList();

            //create the users
            foreach (var u in legacyUsers.OrderBy(a => a.LastName))
            {
                _db.Add(new Publisher { UserId = u.UserId, FirstName = u.FirstName, LastName = u.LastName });
                _db.SaveChanges();
            }

            //create the street territories
            foreach (var t in legacyTerritories.OrderBy(a => a.TerritoryCode))
            {
                var territoryCode = $"{t.TerritoryCode.Substring(0, 1).ToUpper()}-{int.Parse(t.TerritoryCode.Substring(1)):000}";
                var street = new StreetTerritory { TerritoryCode = territoryCode, InActive = t.InActive };
                _db.Add(street);
                _db.SaveChanges();
                foreach (var entry in t.LedgerEntries.OrderBy(x => x.CheckOutDate))
                {
                    street.Activity.Add(new TerritoryActivity
                    {
                        PublisherId = _db.Publishers.FirstOrDefault(x => x.UserId == entry.UserId).PublisherId,
                        CheckOutDate = entry.CheckOutDate,
                        CheckInDate = entry.CheckInDate
                    });
                    _db.SaveChanges();
                }
            }
        }
    }
}
