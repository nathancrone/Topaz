using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class StreetTerritory : Territory
    {
        public StreetTerritory()
        {
            ApartmentTerritories = new List<ApartmentTerritory>();
            InaccessibleTerritories = new List<InaccessibleTerritory>();
        }

        public string MapLocation { get; set; }
        public List<ApartmentTerritory> ApartmentTerritories { get; set; }
        public List<InaccessibleTerritory> InaccessibleTerritories { get; set; }
    }
}
