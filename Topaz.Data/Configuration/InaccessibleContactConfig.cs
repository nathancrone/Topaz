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

            builder.HasMany(x => x.ContactActivity)
                        .WithOne(x => x.Contact)
                        .HasForeignKey(x => x.InaccessibleContactId);

            //builder.HasOne(x => x.AssignPublisher).WithMany(x => x.InaccessibleContacts).HasForeignKey(x => x.AssignPublisherId);
            //builder.HasOne(x => x.ContactList).WithMany(x => x.Contacts).HasForeignKey(x => x.InaccessibleContactListId);
        }
    }
}