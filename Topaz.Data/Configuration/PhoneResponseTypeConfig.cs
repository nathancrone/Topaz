using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Topaz.Common.Models;
using Topaz.Common.Enums;

namespace Topaz.Data.Configuration
{
    internal class PhoneResponseTypeConfig : IEntityTypeConfiguration<PhoneResponseType>
    {
        public void Configure(EntityTypeBuilder<PhoneResponseType> builder)
        {
            builder.HasKey(x => x.PhoneResponseTypeId);
            builder.Property(x => x.PhoneResponseTypeId).ValueGeneratedNever();
            builder.HasData(
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.VoicemailNoName,
                    Name = "Voicemail (no name)"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.VoicemaiNameMatches,
                    Name = "Voicemail (name matches)"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.VoicemailDifferentName,
                    Name = "Voicemail (different name)"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.FaxModem,
                    Name = "Fax / Modem"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.BusySignal,
                    Name = "Busy Signal"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.RingNoAnswer,
                    Name = "Ring no answer"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredNotInterested,
                    Name = "Answered (\"not interested\")"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredDoNotContact,
                    Name = "Answered (\"take me off your list\")"
                }
            );
        }
    }
}