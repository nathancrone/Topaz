using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class DoNotContactPhoneConfig : IEntityTypeConfiguration<DoNotContactPhone>
    {
        public void Configure(EntityTypeBuilder<DoNotContactPhone> builder)
        {
            builder.HasKey(x => x.DoNotContactPhoneId);
            builder.Property(x => x.DoNotContactPhoneId).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.PhoneNumber).IsUnique();
        }
    }
}