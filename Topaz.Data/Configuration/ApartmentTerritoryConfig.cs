using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class ApartmentTerritoryConfig : IEntityTypeConfiguration<ApartmentTerritory>
    {
        public void Configure(EntityTypeBuilder<ApartmentTerritory> builder)
        {
            builder.Property(x => x.StreetTerritoryId).HasColumnName("Apartment_StreetTerritoryId");
            builder.Property(x => x.MapLocation).HasColumnName("Apartment_MapLocation");
            builder.Property(x => x.PropertyName).HasColumnName("Apartment_PropertyName");
            builder.Property(x => x.StreetAddress).HasColumnName("Apartment_StreetAddress");

            builder.HasMany(x => x.Activity)
                .WithOne(x => x.ApartmentTerritory)
                .HasForeignKey(x => x.TerritoryId);
        }
    }
}