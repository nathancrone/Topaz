using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class InaccessibleTerritoryConfig : IEntityTypeConfiguration<InaccessibleTerritory>
    {
        public void Configure(EntityTypeBuilder<InaccessibleTerritory> builder)
        {
            builder.Property(x => x.StreetTerritoryId).HasColumnName("Inaccessible_StreetTerritoryId");

            builder.HasMany(x => x.InaccessibleProperties)
                        .WithOne(x => x.Territory)
                        .HasForeignKey(x => x.TerritoryId);

            builder.HasMany(x => x.Activity)
                .WithOne(x => x.InaccessibleTerritory)
                .HasForeignKey(x => x.TerritoryId);
        }
    }
}