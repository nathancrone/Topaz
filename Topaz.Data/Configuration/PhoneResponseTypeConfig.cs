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
                    Name = "Voicemail (No Name)"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.VoicemaiNameMatches,
                    Name = "Voicemail (Name Matches)"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.VoicemailDifferentName,
                    Name = "Voicemail (Different Name)"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.VoicemailBusiness,
                    Name = "Voicemail (Business Number)"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.NoResponseFaxModem,
                    Name = "No Response (Fax / Modem)"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.NoResponseBusySignal,
                    Name = "No Response (Busy Signal)"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.NoResponseNotWorkingNumber,
                    Name = "No Response (Not a working number)"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.NoResponseRingNoAnswer,
                    Name = "No Response (Ring no answer)"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredRespondedFavorably,
                    Name = "Answered (Responded favorably)"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredImmediateHangup,
                    Name = "Answered (Hung up immediately)"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredNotInterested,
                    Name = "Answered (\"Not Interested\")"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredDoNotContact,
                    Name = "Answered (\"Take me off your list\")"
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredProfanityOrThreatening,
                    Name = "Answered (profanity or threatening)"
                }
            );
        }
    }
}