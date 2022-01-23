using System;
using System.Collections.Generic;

namespace Topaz.UI.ReportShared
{
    public class ReportTerritory
    {
        public int TerritoryId { get; set; }
        public string TerritoryCode { get; set; }
        public IEnumerable<ReportActivity> Activity { get; set; }
    }
}
