namespace Topaz.Common.Enums
{
    public enum PhoneTypeEnum
    {
        Mobile = 1,
        Landline = 2,
        Voip = 3
    }

    public enum ContactActivityTypeEnum
    {
        PhoneWithoutVoicemail = 1,
        PhoneWithVoicemail = 2,
        Letter = 3,
        Email = 4,
        Text = 5
    }

    public enum PhoneReponseTypeEnum
    {
        VoicemailNoName = 100,
        VoicemaiNameMatches = 101,
        VoicemailDifferentName = 102,
        VoicemailBusiness = 103,
        VoicemailFullOrNotSetUp = 104,
        NoResponseFaxModem = 200,
        NoResponseBusySignal = 201,
        NoResponseNotWorkingNumber = 202,
        NoResponseRingNoAnswer = 203,
        AnsweredRespondedFavorably = 300,
        AnsweredNotGoodTime = 301,
        AnsweredImmediateHangup = 302,
        AnsweredNotInterested = 303,
        AnsweredDoNotContact = 304,
        AnsweredProfanityOrThreatening = 305,
        AnsweredNoEnglish = 306
    }
}
