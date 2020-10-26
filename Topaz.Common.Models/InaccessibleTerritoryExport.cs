using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class InaccessibleTerritoryExport
    {
        public int InaccessibleTerritoryExportId { get; set; }
        public int TerritoryId { get; set; }
        public int PublisherId { get; set; }
        public int InaccessibleContactActivityId { get; set; }
        public DateTime ExportDate { get; set; }
        public InaccessibleTerritory Territory { get; set; }
        public Publisher Publisher { get; set; }
        public InaccessibleContactActivity ContactActivity { get; set; }
        public List<InaccessibleTerritoryExportItem> Items { get; set; }
    }
}
