using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class DoNotContactLetterConfig : IEntityTypeConfiguration<DoNotContactLetter>
    {
        public void Configure(EntityTypeBuilder<DoNotContactLetter> builder)
        {
            builder.HasKey(x => x.DoNotContactLetterId);
            builder.Property(x => x.DoNotContactLetterId).ValueGeneratedOnAdd();
            builder.HasIndex(x => new { x.MailingAddress1, x.MailingAddress2 }).IsUnique();
        }
    }
}