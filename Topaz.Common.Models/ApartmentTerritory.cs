using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class ApartmentTerritory : Territory
    {
        public ApartmentTerritory()
        {
            Activity = new List<TerritoryActivity>();
        }
        public int StreetTerritoryId { get; set; }
        public string MapLocation { get; set; }
        public string PropertyName { get; set; }
        public string StreetAddress { get; set; }
        public StreetTerritory StreetTerritory { get; set; }
        public List<TerritoryActivity> Activity { get; set; }
    }
}
