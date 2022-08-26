using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class StreetTerritoryAddressBlock
    {
        public int StreetTerritoryAddressBlockId { get; set; }
        public int TerritoryId { get; set; }
        public string StreetNumbers { get; set; }
        public string Street { get; set; }
        public int EstimatedDwellingCount { get; set; }
        public StreetTerritory Territory { get; set; }
    }
}
