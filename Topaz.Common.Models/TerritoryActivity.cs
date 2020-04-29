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
        public ApartmentTerritory ApartmentTerritory { get; set; }
        public BusinessTerritory BusinessTerritory { get; set; }
        public InaccessibleTerritory InaccessibleTerritory { get; set; }
        public StreetTerritory StreetTerritory { get; set; }
        public Publisher Publisher { get; set; }
    }
}
