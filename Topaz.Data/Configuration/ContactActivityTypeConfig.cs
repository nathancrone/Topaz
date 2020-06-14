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
                    ContactActivityTypeId = (int)ContactActivityTypeEnum.PhoneWithoutVoicemail,
                    Name = "Phone (don't leave a voicemail)"
                },
                new ContactActivityType
                {
                    ContactActivityTypeId = (int)ContactActivityTypeEnum.PhoneWithVoicemail,
                    Name = "Phone (leave a voicemail)"
                },
                new ContactActivityType
                {
                    ContactActivityTypeId = (int)ContactActivityTypeEnum.Letter,
                    Name = "Letter"
                },
                new ContactActivityType
                {
                    ContactActivityTypeId = (int)ContactActivityTypeEnum.Email,
                    Name = "Email"
                },
                new ContactActivityType
                {
                    ContactActivityTypeId = (int)ContactActivityTypeEnum.Text,
                    Name = "Text"
                }
            );
        }
    }
}