using System;

namespace Topaz.Common.Models
{
    public class TerritoryActivity
    {
        public int TerritoryActivityId { get; set; }
        public int TerritoryId { get; set; }
        public int PublisherId { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public string Notes { get; set; }
        public Territory Territory { get; set; }
        public Publisher Publisher { get; set; }
    }
}
