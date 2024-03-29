using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class StreetTerritoryConfig : IEntityTypeConfiguration<StreetTerritory>
    {
        public void Configure(EntityTypeBuilder<StreetTerritory> builder)
        {
            builder.Property(x => x.MapLocation).HasColumnName("Street_MapLocation");
            builder.Property(x => x.RefId).HasColumnName("Street_RefId");

            builder.HasMany(x => x.InaccessibleTerritories)
                .WithOne(x => x.StreetTerritory)
                .HasForeignKey(x => x.StreetTerritoryId);

            builder.HasMany(x => x.ApartmentTerritories)
                .WithOne(x => x.StreetTerritory)
                .HasForeignKey(x => x.StreetTerritoryId);

            builder.HasMany(x => x.Activity)
                .WithOne(x => x.StreetTerritory)
                .HasForeignKey(x => x.TerritoryId);

            builder.HasMany(x => x.StreetDoNotContacts)
                .WithOne(x => x.Territory)
                .HasForeignKey(x => x.TerritoryId);

            builder.HasMany(x => x.LetterDoNotContacts)
                .WithOne(x => x.Territory)
                .HasForeignKey(x => x.TerritoryId);

            builder.HasMany(x => x.AddressBlocks)
                .WithOne(x => x.Territory)
                .HasForeignKey(x => x.TerritoryId);
        }
    }
}