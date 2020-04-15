using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;

namespace Topaz.Data.Configuration
{
    internal class InaccessibleContactListConfig : IEntityTypeConfiguration<InaccessibleContactList>
    {
        public void Configure(EntityTypeBuilder<InaccessibleContactList> builder)
        {
            builder.HasKey(x => x.InaccessibleContactListId);
            builder.Property(x => x.InaccessibleContactListId).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Contacts)
                .WithOne(x => x.ContactList)
                .HasForeignKey(x => x.InaccessibleContactListId);
        }
    }
}