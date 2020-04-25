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
        Phone = 1,
        Text = 2,
        Email = 3,
        Letter = 4
    }

    public enum PhoneReponseTypeEnum
    {
        VoicemailNoName = 100,
        VoicemaiNameMatches = 101,
        VoicemailDifferentName = 102,
        VoicemailBusiness = 103,
        NoResponseFaxModem = 200,
        NoResponseBusySignal = 201,
        NoResponseNotWorkingNumber = 202,
        NoResponseRingNoAnswer = 203,
        AnsweredRespondedFavorably = 300,
        AnsweredNotGoodTime = 301,
        AnsweredImmediateHangup = 302,
        AnsweredNotInterested = 303,
        AnsweredDoNotContact = 304,
        AnsweredProfanityOrThreatening = 305
    }
}
