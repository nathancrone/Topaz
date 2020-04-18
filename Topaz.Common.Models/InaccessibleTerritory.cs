using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class InaccessibleTerritory : Territory
    {
        public InaccessibleTerritory()
        {
            InaccessibleAddresses = new List<InaccessibleAddress>();
        }
        public int StreetTerritoryId { get; set; }
        public StreetTerritory StreetTerritory { get; set; }
        public List<InaccessibleAddress> InaccessibleAddresses { get; set; }
    }
}
