﻿using System;
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
            serviceProvider.GetService<ConsoleApp>().Legacy();
            serviceProvider.GetService<ConsoleApp>().Populate();
            Console.WriteLine("done");
            Console.ReadLine();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<LegacyDbContext, LegacyDbContext>();
            services.AddDbContext<LegacyDbContext>(options => options.UseSqlite("Data Source=JasperDB.db"));
            services.AddTransient<SourceDbContext, SourceDbContext>();
            services.AddDbContext<SourceDbContext>(options => options.UseSqlite("Data Source=TopazDb.src.db"));
            services.AddTransient<TargetDbContext, TargetDbContext>();
            services.AddDbContext<TargetDbContext>(options => options.UseSqlite("Data Source=TopazDb.db"));
            // IMPORTANT! Register our application entry point
            services.AddSingleton<ConsoleApp>();
            return services;
        }
    }
    public class ConsoleApp
    {
        private TopazDbContext _sourceDb;
        private TopazDbContext _targetDb;
        private LegacyDbContext _legacyDb;

        private Dictionary<string, int> mapLegacyUserTargetPublisher;
        private Dictionary<int, int> mapSourcePublisherTargetPublisher;
        private Dictionary<int, int> mapLegacyTerritoryTargetStreetTerritory;

        public ConsoleApp(LegacyDbContext legacyDb, SourceDbContext sourceDb, TargetDbContext targetDb)
        {
            _legacyDb = legacyDb;
            _sourceDb = sourceDb;
            _targetDb = targetDb;
        }

        // Application starting point
        public void Legacy()
        {
            List<LegacyTerritory> legacyTerritories = new List<LegacyTerritory>();

            // get the legacy data out of the database
            legacyTerritories = _legacyDb.LegacyTerritories.Include(a => a.LedgerEntries).ThenInclude(a => a.User).AsNoTracking().ToList();

            // distinct list of legacy users
            var legacyUsers = legacyTerritories.SelectMany(a => a.LedgerEntries.Select(b => new { b.User.UserId, b.User.FirstName, b.User.LastName })).Distinct().ToList();

            mapLegacyUserTargetPublisher = new Dictionary<string, int>();
            // create the publishers in the target db
            foreach (var u in legacyUsers.OrderBy(a => a.LastName))
            {
                var pub = new Publisher { FirstName = u.FirstName, LastName = u.LastName };
                _targetDb.Add(pub);
                _targetDb.SaveChanges();
                mapLegacyUserTargetPublisher.Add(u.UserId, pub.PublisherId);
            }

            mapSourcePublisherTargetPublisher = new Dictionary<int, int>();
            // create the publishers in the target db
            foreach (var srcPub in _sourceDb.Publishers.OrderBy(a => a.LastName))
            {
                if (!_targetDb.Publishers.Any(x => x.FirstName == srcPub.FirstName && x.LastName == srcPub.LastName))
                {
                    var pub = new Publisher { FirstName = srcPub.FirstName, LastName = srcPub.LastName };
                    _targetDb.Add(pub);
                    _targetDb.SaveChanges();
                    mapSourcePublisherTargetPublisher.Add(srcPub.PublisherId, pub.PublisherId);
                }
                else
                {
                    foreach (var pub in _targetDb.Publishers.Where(x => x.FirstName == srcPub.FirstName && x.LastName == srcPub.LastName))
                    {
                        mapSourcePublisherTargetPublisher.Add(srcPub.PublisherId, pub.PublisherId);
                    }
                }
            }

            mapLegacyTerritoryTargetStreetTerritory = new Dictionary<int, int>();
            // create the street territories
            foreach (var t in legacyTerritories.OrderBy(a => a.TerritoryCode))
            {
                var territoryCode = $"{t.TerritoryCode.Substring(0, 1).ToUpper()}-{int.Parse(t.TerritoryCode.Substring(1)):000}";
                var street = new StreetTerritory { TerritoryCode = territoryCode, InActive = t.InActive, RefId = t.TerritoryId };
                _targetDb.Add(street);
                _targetDb.SaveChanges();
                mapLegacyTerritoryTargetStreetTerritory.Add(t.TerritoryId, street.TerritoryId);
                foreach (var entry in t.LedgerEntries.OrderBy(x => x.CheckOutDate))
                {
                    street.Activity.Add(new TerritoryActivity
                    {
                        PublisherId = mapLegacyUserTargetPublisher[entry.UserId],
                        CheckOutDate = entry.CheckOutDate,
                        CheckInDate = entry.CheckInDate
                    });
                    _targetDb.SaveChanges();
                }
            }
        }

        public void Populate()
        {
            // copy inaccessible territories from source
            var sourceTerritories = _sourceDb.InaccessibleTerritories
                .Include(x => x.StreetTerritory)
                .Include(x => x.InaccessibleProperties)
                .ThenInclude(x => x.ContactLists)
                .ThenInclude(x => x.Contacts)
                .ThenInclude(x => x.ContactActivity)
                .ThenInclude(x => x.Publisher)
                .AsNoTracking()
                .ToList();

            foreach (var t in sourceTerritories)
            {
                var streetTerritoryId = _targetDb.StreetTerritories.Where(x => x.TerritoryCode == t.StreetTerritory.TerritoryCode).Select(x => x.TerritoryId).FirstOrDefault();

                var territory = new InaccessibleTerritory
                {
                    TerritoryCode = t.TerritoryCode,
                    StreetTerritoryId = streetTerritoryId,
                    InActive = t.InActive
                };

                foreach (var p in t.InaccessibleProperties)
                {
                    var property = new InaccessibleProperty
                    {
                        ResearchedDate = p.ResearchedDate,
                        StreetNumbers = p.StreetNumbers,
                        Street = p.Street,
                        City = p.City,
                        State = p.State,
                        PostalCode = p.PostalCode,
                        EstimatedDwellingCount = p.EstimatedDwellingCount,
                        PropertyName = p.PropertyName,
                        Description = p.Description
                    };

                    foreach (var l in p.ContactLists)
                    {
                        var list = new InaccessibleContactList
                        {
                            CreateDate = l.CreateDate
                        };

                        foreach (var c in l.Contacts)
                        {
                            int? assign = null;
                            if (c.AssignPublisherId.HasValue)
                            {
                                assign = mapSourcePublisherTargetPublisher[c.AssignPublisherId.Value];
                            }

                            var contact = new InaccessibleContact
                            {
                                AssignPublisherId = assign,
                                AssignDate = c.AssignDate,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                MiddleInitial = c.MiddleInitial,
                                Age = c.Age,
                                MailingAddress1 = c.MailingAddress1,
                                MailingAddress2 = c.MailingAddress2,
                                City = c.City,
                                State = c.State,
                                PostalCode = c.PostalCode,
                                PhoneTypeId = c.PhoneTypeId,
                                PhoneNumber = c.PhoneNumber,
                                EmailAddresses = c.EmailAddresses,
                            };

                            foreach (var a in c.ContactActivity)
                            {
                                int pub = mapSourcePublisherTargetPublisher[a.PublisherId];

                                contact.ContactActivity.Add(new InaccessibleContactActivity
                                {
                                    PublisherId = pub,
                                    ActivityDate = a.ActivityDate,
                                    ContactActivityTypeId = a.ContactActivityTypeId,
                                    PhoneCallerIdBlocked = a.PhoneCallerIdBlocked,
                                    PhoneResponseTypeId = a.PhoneResponseTypeId,
                                    Notes = a.Notes
                                });
                            }
                            list.Contacts.Add(contact);
                        }
                        property.ContactLists.Add(list);
                    }
                    territory.InaccessibleProperties.Add(property);
                }
                _targetDb.Add(territory);
            }

            _targetDb.SaveChanges();

            //this will set the current contact list for the property
            _targetDb.Database.ExecuteSqlRaw(@"
                UPDATE 
                InaccessibleProperties 
                SET 
                CurrentContactListId = (
                    SELECT MAX(InaccessibleContactListId)
                    FROM InaccessibleContactLists i
                    WHERE i.InaccessiblePropertyId = InaccessibleProperties.InaccessiblePropertyId 
                )"
            );

            var srcTerritories = _sourceDb.InaccessibleTerritories.Include(x => x.Activity);

            foreach (var srcTerritory in srcTerritories)
            {
                var targetTerritory = _targetDb.InaccessibleTerritories.Where(x => x.TerritoryCode == srcTerritory.TerritoryCode).FirstOrDefault();
                foreach (var srcActivity in srcTerritory.Activity)
                {
                    targetTerritory.Activity.Add(new TerritoryActivity
                    {
                        PublisherId = mapSourcePublisherTargetPublisher[srcActivity.PublisherId],
                        CheckOutDate = srcActivity.CheckOutDate,
                        CheckInDate = srcActivity.CheckInDate,
                        Notes = srcActivity.Notes
                    });
                }
                _targetDb.SaveChanges();
            }

        }
    }
}
