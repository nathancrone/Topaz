using System;

namespace Topaz.Common.Models
{
    public class DoNotContactLetter
    {
        public int DoNotContactLetterId { get; set; }
        public int TerritoryId { get; set; }
        public int PublisherId { get; set; }
        public DateTime? ReportedDate { get; set; }
        public string MailingAddress1 { get; set; }
        public string MailingAddress2 { get; set; }
        public string Notes { get; set; }
        public StreetTerritory Territory { get; set; }
        public Publisher Publisher { get; set; }
    }
}
