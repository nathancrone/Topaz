using System;

namespace Topaz.Common.Models
{
    public class DoNotContactLetter
    {
        public int DoNotContactLetterId { get; set; }
        public DateTime? ReportedDate { get; set; }
        public string MailingAddress { get; set; }
        public string Notes { get; set; }
    }
}
