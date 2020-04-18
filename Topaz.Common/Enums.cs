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
        AnsweredImmediateHangup = 10,
        AnsweredNotInterested = 11,
        AnsweredDoNotContact = 12,
        AnsweredProfanityOrThreatening = 13

    }
}
