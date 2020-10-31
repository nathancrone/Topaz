using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class InaccessibleTerritoryExportItemConfig : IEntityTypeConfiguration<InaccessibleTerritoryExportItem>
    {
        public void Configure(EntityTypeBuilder<InaccessibleTerritoryExportItem> builder)
        {
            builder.HasKey(x => x.InaccessibleTerritoryExportItemId);
            builder.Property(x => x.InaccessibleTerritoryExportItemId).ValueGeneratedOnAdd();

            builder.HasOne<InaccessibleContact>(x => x.Contact)
                .WithOne(x => x.ExportItem)
                .HasForeignKey<InaccessibleContact>(x => x.InaccessibleTerritoryExportItemId);
        }
    }
}