using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class InaccessibleAddressConfig : IEntityTypeConfiguration<InaccessibleAddress>
    {
        public void Configure(EntityTypeBuilder<InaccessibleAddress> builder)
        {
            builder.HasKey(x => x.InaccessibleAddressId);
            builder.Property(x => x.InaccessibleAddressId).ValueGeneratedOnAdd();

            builder.HasMany(x => x.ContactLists)
                        .WithOne(x => x.Address)
                        .HasForeignKey(x => x.InaccessibleAddressId);
        }
    }
}