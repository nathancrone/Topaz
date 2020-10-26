using System;

namespace Topaz.Common.Models
{
    public class InaccessibleTerritoryExportItem
    {
        public int InaccessibleTerritoryExportItemId { get; set; }
        public int InaccessibleTerritoryExportId { get; set; }
        public int InaccessibleContactId { get; set; }
        public InaccessibleContact Contact { get; set; }
        public InaccessibleTerritoryExport Export { get; set; }
    }
}
