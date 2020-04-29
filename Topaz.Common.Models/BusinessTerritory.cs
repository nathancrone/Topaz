using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class BusinessTerritory : Territory
    {
        public BusinessTerritory()
        {
            Activity = new List<TerritoryActivity>();
        }
        public string MapLocation { get; set; }
        public List<TerritoryActivity> Activity { get; set; }

    }
}
