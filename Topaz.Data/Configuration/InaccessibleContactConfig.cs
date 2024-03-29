using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class InaccessibleContactConfig : IEntityTypeConfiguration<InaccessibleContact>
    {
        public void Configure(EntityTypeBuilder<InaccessibleContact> builder)
        {
            builder.HasKey(x => x.InaccessibleContactId);
            builder.Property(x => x.InaccessibleContactId).ValueGeneratedOnAdd();
            builder.Property(x => x.IsAvailable).HasDefaultValue(false);

            builder.Ignore(x => x.DoNotContactPhone);
            builder.Ignore(x => x.DoNotContactLetter);
            builder.Ignore(x => x.AssignedDays);
            builder.Ignore(x => x.ActivityDays);

            builder.HasMany(x => x.ContactActivity)
                        .WithOne(x => x.Contact)
                        .HasForeignKey(x => x.InaccessibleContactId);

            builder.HasOne<InaccessibleTerritoryExportItem>(x => x.ExportItem)
                .WithOne(x => x.Contact)
                .HasForeignKey<InaccessibleTerritoryExportItem>(x => x.InaccessibleContactId);
        }
    }
}