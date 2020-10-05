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
            Activity = new List<TerritoryActivity>();
        }

        public string MapLocation { get; set; }
        public List<ApartmentTerritory> ApartmentTerritories { get; set; }
        public List<InaccessibleTerritory> InaccessibleTerritories { get; set; }
        public int? RefId { get; set; }
        public List<TerritoryActivity> Activity { get; set; }
        public List<DoNotContactStreet> StreetDoNotContacts { get; set; }
        public List<DoNotContactLetter> LetterDoNotContacts { get; set; }
    }
}
