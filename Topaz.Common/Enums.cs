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
        Letter = 2
    }

    public enum PhoneReponseTypeEnum
    {
        VoicemailNoName = 1,
        VoicemaiNameMatches = 2,
        VoicemailDifferentName = 3,
        VoicemailBusiness = 4,
        NoResponseFaxModem = 5,
        NoResponseBusySignal = 6,
        NoResponseNotWorkingNumber = 7,
        NoResponseRingNoAnswer = 8,
        AnsweredRespondedFavorably = 9,
        AnsweredNotGoodTime = 10,
        AnsweredImmediateHangup = 11,
        AnsweredNotInterested = 12,
        AnsweredDoNotContact = 13,
        AnsweredProfanityOrThreatening = 14

    }
}
