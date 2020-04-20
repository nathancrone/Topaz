using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class PublisherConfig : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(x => x.PublisherId);
            builder.Property(x => x.PublisherId).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Activity)
            .WithOne(x => x.Publisher)
            .HasForeignKey(x => x.PublisherId);

            builder.HasMany(x => x.InaccessibleContacts)
            .WithOne(x => x.Publisher)
            .HasForeignKey(x => x.PublisherId);

            builder.HasMany(x => x.InaccessibleContactActivity)
            .WithOne(x => x.Publisher)
            .HasForeignKey(x => x.PublisherId);
        }
    }
}