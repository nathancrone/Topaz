using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class DoNotContactStreetConfig : IEntityTypeConfiguration<DoNotContactStreet>
    {
        public void Configure(EntityTypeBuilder<DoNotContactStreet> builder)
        {
            builder.HasKey(x => x.DoNotContactStreetId);
            builder.Property(x => x.DoNotContactStreetId).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.StreetAddress).IsUnique();
        }
    }
}