using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class StreetTerritoryAddressBlockConfig : IEntityTypeConfiguration<StreetTerritoryAddressBlock>
    {
        public void Configure(EntityTypeBuilder<StreetTerritoryAddressBlock> builder)
        {
            builder.HasKey(x => x.StreetTerritoryAddressBlockId);
            builder.Property(x => x.StreetTerritoryAddressBlockId).ValueGeneratedOnAdd();
        }
    }
}