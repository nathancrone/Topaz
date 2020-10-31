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
        None = -1,
        PhoneWithoutVoicemail = 1,
        PhoneWithVoicemail = 2,
        Letter = 3,
        LetterReturned = 4,
        Email = 5,
        Text = 6,
        Export = 8
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
        NoResponseNotAcceptingCalls = 204,
        AnsweredRespondedFavorably = 300,
        AnsweredNotGoodTime = 301,
        AnsweredHangUpImmediate = 302,
        AnsweredHangUpAfterListening = 303,
        AnsweredNotInterested = 304,
        AnsweredDoNotContact = 305,
        AnsweredProfanityOrThreatening = 306,
        AnsweredNoEnglish = 307,
        AnsweredBusiness = 308
    }
}
