using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class BusinessTerritoryConfig : IEntityTypeConfiguration<BusinessTerritory>
    {
        public void Configure(EntityTypeBuilder<BusinessTerritory> builder)
        {
            builder.Property(x => x.MapLocation).HasColumnName("Business_MapLocation");
        }
    }
}