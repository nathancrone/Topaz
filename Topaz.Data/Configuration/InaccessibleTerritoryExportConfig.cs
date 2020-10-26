using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class InaccessibleTerritoryExportConfig : IEntityTypeConfiguration<InaccessibleTerritoryExport>
    {
        public void Configure(EntityTypeBuilder<InaccessibleTerritoryExport> builder)
        {
            builder.HasKey(x => x.InaccessibleTerritoryExportId);
            builder.Property(x => x.InaccessibleTerritoryExportId).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Items)
                        .WithOne(x => x.Export)
                        .HasForeignKey(x => x.InaccessibleTerritoryExportId);
        }
    }
}