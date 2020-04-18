using System;
using Microsoft.EntityFrameworkCore;
using Topaz.UI.MigrationConsole.Legacy.Models;

namespace Topaz.UI.MigrationConsole.Legacy
{
    public class LegacyDbContext : DbContext
    {
        public LegacyDbContext(DbContextOptions<LegacyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LegacyTerritory>().HasKey(x => x.TerritoryId);

            modelBuilder.Entity<LegacyTerritory>()
                .ToTable("tblTerritories")
                .Property(x => x.TerritoryId).ValueGeneratedOnAdd();

            modelBuilder.Entity<LegacyTerritory>()
                .Property(x => x.TerritoryCode)
                .IsRequired();

            modelBuilder.Entity<LegacyTerritory>()
                .HasMany(x => x.LedgerEntries)
                .WithOne(x => x.Territory);

            modelBuilder.Entity<LegacyLedgerEntry>().HasKey(x => x.LedgerEntryId);

            modelBuilder.Entity<LegacyLedgerEntry>()
                .ToTable("tblLedgerEntries")
                .Property(x => x.LedgerEntryId).ValueGeneratedOnAdd();

            modelBuilder.Entity<LegacyLedgerEntry>()
                .Property(x => x.TerritoryId)
                .IsRequired();

            modelBuilder.Entity<LegacyLedgerEntry>()
                .Property(x => x.UserId)
                .IsRequired();

            modelBuilder.Entity<LegacyUser>().HasKey(x => x.UserId);

            modelBuilder.Entity<LegacyUser>()
                .ToTable("tblUsers")
                .HasMany(x => x.LedgerEntries)
                .WithOne(x => x.User).HasForeignKey(x => x.UserId);
        }

        public DbSet<LegacyTerritory> LegacyTerritories { get; set; }
        public DbSet<LegacyLedgerEntry> LegacyLedgerEntries { get; set; }

    }
}