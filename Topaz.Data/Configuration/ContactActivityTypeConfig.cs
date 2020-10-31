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
                    Name = "Phone (don't leave a voicemail)",
                    Description = "Contact this person via telephone. DO NOT leave a message if the phone call goes to voicemail."
                },
                new ContactActivityType
                {
                    ContactActivityTypeId = (int)ContactActivityTypeEnum.PhoneWithVoicemail,
                    Name = "Phone (leave a voicemail)",
                    Description = "Contact this person via telephone. Leave a message if the phone call goes to voicemail."
                },
                new ContactActivityType
                {
                    ContactActivityTypeId = (int)ContactActivityTypeEnum.Letter,
                    Name = "Letter Sent",
                    Description = "Write a letter to this person."
                },
                new ContactActivityType
                {
                    ContactActivityTypeId = (int)ContactActivityTypeEnum.LetterReturned,
                    Name = "Letter Returned",
                    Description = "Designates that the letter was returned without reaching the intended recipient."
                },
                new ContactActivityType
                {
                    ContactActivityTypeId = (int)ContactActivityTypeEnum.Email,
                    Name = "Email",
                    Description = "Send an email to this person."
                },
                new ContactActivityType
                {
                    ContactActivityTypeId = (int)ContactActivityTypeEnum.Text,
                    Name = "Text",
                    Description = "Send a text message to this person."
                },
                new ContactActivityType
                {
                    ContactActivityTypeId = (int)ContactActivityTypeEnum.Export,
                    Name = "Export",
                    Description = "Contact exported to be worked externally."
                }
            );
        }
    }
}