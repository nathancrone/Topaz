using System;

namespace Topaz.Common.Models
{
    public class InaccessibleContactActivity
    {
        public int InaccessibleContactActivityId { get; set; }
        public int InaccessibleContactId { get; set; }
        public DateTime? ActivityDate { get; set; }
        public int ContactActivityTypeId { get; set; }
        public bool LetterReturned { get; set; }
        public string Notes { get; set; }
        public InaccessibleContact Contact { get; set; }
        public ContactActivityType ContactActivityType { get; set; }
        public bool PhoneCallerIdBlocked { get; set; }
        public int PhoneResponseTypeId { get; set; }
        public PhoneResponseType PhoneResponseType { get; set; }

    }
}
