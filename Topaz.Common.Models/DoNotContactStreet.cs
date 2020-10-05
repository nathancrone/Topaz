using System;

namespace Topaz.Common.Models
{
    public class DoNotContactStreet
    {
        public int DoNotContactStreetId { get; set; }
        public int TerritoryId { get; set; }
        public int PublisherId { get; set; }
        public DateTime? ReportedDate { get; set; }
        public string StreetAddress { get; set; }
        public string Coordinates { get; set; }
        public string Notes { get; set; }
        public StreetTerritory Territory { get; set; }
        public Publisher Publisher { get; set; }
    }
}
