using System;

namespace Topaz.Common.Models
{
    public class DoNotContactPhone
    {
        public int DoNotContactPhoneId { get; set; }
        public DateTime? ReportedDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Notes { get; set; }
    }
}
