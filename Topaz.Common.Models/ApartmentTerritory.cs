using System;

namespace Topaz.Common.Models
{
    public class ApartmentTerritory : Territory
    {
        public int StreetTerritoryId { get; set; }
        public string MapLocation { get; set; }
        public string PropertyName { get; set; }
        public string StreetAddress { get; set; }
    }
}
