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
                    Name = "Voicemail (No Name)",
                    Description = "The call went to voicemail. The voicemail message did not give a name."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.VoicemaiNameMatches,
                    Name = "Voicemail (Name Matches)",
                    Description = "The call went to voicemail. The name given in the voicemail message matches the contact information."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.VoicemailDifferentName,
                    Name = "Voicemail (Different Name)",
                    Description = "The call went to voicemail. The name given in the voicemail message is different from the contact information."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.VoicemailBusiness,
                    Name = "Voicemail (Business Number)",
                    Description = "The call went to voicemail. The voicemail message was for a business."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.VoicemailFullOrNotSetUp,
                    Name = "Voicemail (Mailbox Full or Not Set Up)",
                    Description = "The call went to voicemail but the voicemail account was either not set up or was full."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.NoResponseFaxModem,
                    Name = "No Response (Fax / Modem)",
                    Description = "The call attempt was unsuccessful. The caller heard a fax or modem signal."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.NoResponseBusySignal,
                    Name = "No Response (Busy Signal)",
                    Description = "The call attempt was unsuccessful. The caller heard a busy signal."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.NoResponseNotWorkingNumber,
                    Name = "No Response (Not a working number)",
                    Description = "The call attempt was unsuccessful. The caller got an automated message indicating that this is not a working number."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.NoResponseRingNoAnswer,
                    Name = "No Response (Ring no answer)",
                    Description = "The call attempt was unsuccessful. The caller let the phone ring multiple times but nobody answered."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.NoResponseNotAcceptingCalls,
                    Name = "No Response (Ring no answer)",
                    Description = "The call attempt was unsuccessful. Message indicating that the number is not accepting calls."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredRespondedFavorably,
                    Name = "Answered (Responded favorably)",
                    Description = "The caller successfully spoke to a person. The call was positive. This contact will be considered complete. The caller will retain this call for their personal records if they feel a follow up would be appropriate."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredNotGoodTime,
                    Name = "Answered (\"Not a good time\")",
                    Description = "The caller successfully spoke to a person. The contact stated that they weren't able to talk right now. A call back later would be appropriate."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredHangUpImmediate,
                    Name = "Answered (Hung up immediately)",
                    Description = "Someone picked up the phone. The call immediately disconnected (the caller likely hung up instantly after answering). No communication occurred."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredHangUpAfterListening,
                    Name = "Answered (Listened then hung up)",
                    Description = "Someone picked up the phone. You were able to introduce yourself. The person hung up."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredNotInterested,
                    Name = "Answered (\"Not Interested\")",
                    Description = "Someone picked up the phone. The contact indicated that they weren't interested."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredDoNotContact,
                    Name = "Answered (\"Take me off your list\")",
                    Description = "Someone picked up the phone. The contact specifically requested to be removed from the calling list."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredProfanityOrThreatening,
                    Name = "Answered (profanity or threatening)",
                    Description = "Someone picked up the phone. The person was agitated. They possibly used rude, threatening, or profane language."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredNoEnglish,
                    Name = "Answered (doesn't speak English)",
                    Description = "Someone picked up the phone. Was unable to communicate because they didn't speak English."
                },
                new PhoneResponseType
                {
                    PhoneResponseTypeId = (int)PhoneReponseTypeEnum.AnsweredBusiness,
                    Name = "Answered (Business)",
                    Description = "Someone picked up the phone. The contact was a business and was unable to speak."
                }
            );
        }
    }
}