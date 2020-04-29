using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class InaccessibleTerritory : Territory
    {
        public InaccessibleTerritory()
        {
            InaccessibleProperties = new List<InaccessibleProperty>();
            Activity = new List<TerritoryActivity>();
        }
        public int StreetTerritoryId { get; set; }
        public StreetTerritory StreetTerritory { get; set; }
        public List<InaccessibleProperty> InaccessibleProperties { get; set; }
        public List<TerritoryActivity> Activity { get; set; }
    }
}
