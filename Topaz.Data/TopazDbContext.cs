using System;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data.Configuration;

namespace Topaz.Data
{
    public class TopazDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=TopazDb.db");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.ApplyConfiguration(new ApartmentTerritoryConfig());
            modelBuilder.ApplyConfiguration(new BusinessTerritoryConfig());
            modelBuilder.ApplyConfiguration(new InaccessibleAddressConfig());
            modelBuilder.ApplyConfiguration(new InaccessibleContactActivityConfig());
            modelBuilder.ApplyConfiguration(new InaccessibleContactConfig());
            modelBuilder.ApplyConfiguration(new InaccessibleContactListConfig());
            modelBuilder.ApplyConfiguration(new InaccessibleTerritoryConfig());
            modelBuilder.ApplyConfiguration(new PhoneResponseTypeConfig());
            modelBuilder.ApplyConfiguration(new ContactActivityTypeConfig());
            modelBuilder.ApplyConfiguration(new PhoneTypeConfig());
            modelBuilder.ApplyConfiguration(new PublisherConfig());
            modelBuilder.ApplyConfiguration(new StreetTerritoryConfig());
            modelBuilder.ApplyConfiguration(new TerritoryActivityConfig());
            modelBuilder.ApplyConfiguration(new TerritoryConfig());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Territory> Territories { get; set; }
        public DbSet<TerritoryActivity> TerritoryActivities { get; set; }
        public DbSet<BusinessTerritory> BusinessTerritories { get; set; }
        public DbSet<StreetTerritory> StreetTerritories { get; set; }
        public DbSet<ApartmentTerritory> ApartmentTerritories { get; set; }
        public DbSet<InaccessibleTerritory> InaccessibleTerritories { get; set; }
        public DbSet<InaccessibleAddress> InaccessibleAddresses { get; set; }
        public DbSet<InaccessibleContactList> InaccessibleContactLists { get; set; }
        public DbSet<InaccessibleContact> InaccessibleContacts { get; set; }
        public DbSet<InaccessibleContactActivity> InaccessibleContactActivities { get; set; }
        public DbSet<DoNotContactStreet> DoNotContactStreets { get; set; }
        public DbSet<DoNotContactPhone> DoNotContactPhones { get; set; }
        public DbSet<DoNotContactLetter> DoNotContactLetters { get; set; }
        public DbSet<PhoneResponseType> PhoneResponseTypes { get; set; }
        public DbSet<ContactActivityType> ContactActivityTypes { get; set; }
        public DbSet<PhoneType> PhoneType { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
    }
}
