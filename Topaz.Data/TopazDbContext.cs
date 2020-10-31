using System;
using Microsoft.EntityFrameworkCore;
using Topaz.Common.Models;
using Topaz.Data.Configuration;

namespace Topaz.Data
{
    public class TopazDbContext : DbContext
    {
        protected TopazDbContext(DbContextOptions options) : base(options) { }
        public TopazDbContext(DbContextOptions<TopazDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApartmentTerritoryConfig());
            modelBuilder.ApplyConfiguration(new BusinessTerritoryConfig());
            modelBuilder.ApplyConfiguration(new ContactActivityTypeConfig());
            modelBuilder.ApplyConfiguration(new DoNotContactLetterConfig());
            modelBuilder.ApplyConfiguration(new DoNotContactPhoneConfig());
            modelBuilder.ApplyConfiguration(new DoNotContactStreetConfig());
            modelBuilder.ApplyConfiguration(new InaccessibleContactActivityConfig());
            modelBuilder.ApplyConfiguration(new InaccessibleContactConfig());
            modelBuilder.ApplyConfiguration(new InaccessibleContactListConfig());
            modelBuilder.ApplyConfiguration(new InaccessiblePropertyConfig());
            modelBuilder.ApplyConfiguration(new InaccessibleTerritoryConfig());
            modelBuilder.ApplyConfiguration(new PhoneResponseTypeConfig());
            modelBuilder.ApplyConfiguration(new PhoneTypeConfig());
            modelBuilder.ApplyConfiguration(new PublisherConfig());
            modelBuilder.ApplyConfiguration(new StreetTerritoryConfig());
            modelBuilder.ApplyConfiguration(new TerritoryActivityConfig());
            modelBuilder.ApplyConfiguration(new TerritoryConfig());
            modelBuilder.ApplyConfiguration(new InaccessibleTerritoryExportConfig());
            modelBuilder.ApplyConfiguration(new InaccessibleTerritoryExportItemConfig());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Territory> Territories { get; set; }
        public DbSet<TerritoryActivity> TerritoryActivities { get; set; }
        public DbSet<BusinessTerritory> BusinessTerritories { get; set; }
        public DbSet<StreetTerritory> StreetTerritories { get; set; }
        public DbSet<ApartmentTerritory> ApartmentTerritories { get; set; }
        public DbSet<InaccessibleTerritory> InaccessibleTerritories { get; set; }
        public DbSet<InaccessibleProperty> InaccessibleProperties { get; set; }
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
        public DbSet<InaccessibleTerritoryExport> InaccessibleTerritoryExports { get; set; }
        public DbSet<InaccessibleTerritoryExportItem> InaccessibleTerritoryExportItems { get; set; }
    }
}
