using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;
using Topaz.Common.Enums;

namespace Topaz.Data.Configuration
{
    internal class ContactActivityTypeConfig : IEntityTypeConfiguration<ContactActivityType>
    {
        public void Configure(EntityTypeBuilder<ContactActivityType> builder)
        {
            builder.HasKey(x => x.ContactActivityTypeId);
            builder.Property(x => x.ContactActivityTypeId).ValueGeneratedNever();
            builder.HasData(
                new ContactActivityType
                {
                    ContactActivityTypeId = (int)ContactActivityTypeEnum.Phone,
                    Name = "Phone"
                },
                new ContactActivityType
                {
                    ContactActivityTypeId = (int)ContactActivityTypeEnum.Letter,
                    Name = "Letter"
                }
            );
        }
    }
}