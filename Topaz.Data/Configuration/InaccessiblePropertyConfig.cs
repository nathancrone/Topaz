using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class InaccessiblePropertyConfig : IEntityTypeConfiguration<InaccessibleProperty>
    {
        public void Configure(EntityTypeBuilder<InaccessibleProperty> builder)
        {
            builder.HasKey(x => x.InaccessiblePropertyId);
            builder.Property(x => x.InaccessiblePropertyId).ValueGeneratedOnAdd();

            builder.HasMany(x => x.ContactLists)
                        .WithOne(x => x.Property)
                        .HasForeignKey(x => x.InaccessiblePropertyId);
        }
    }
}