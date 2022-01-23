using System;

namespace Topaz.UI.ReportShared
{
    public class ReportActivity
    {
        public int TerritoryActivityId { get; set; }
        public int TerritoryId { get; set; }
        public int PublisherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime? CheckInDate { get; set; }
    }
}
