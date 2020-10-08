using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class TerritoryConfig : IEntityTypeConfiguration<Territory>
    {
        public void Configure(EntityTypeBuilder<Territory> builder)
        {
            builder.HasKey(x => x.TerritoryId);
            builder.Property(x => x.TerritoryId).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.TerritoryCode).IsUnique();
        }
    }
}