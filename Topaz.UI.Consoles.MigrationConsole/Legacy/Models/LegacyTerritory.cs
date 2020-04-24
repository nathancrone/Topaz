using System;
using System.Collections.Generic;

namespace Topaz.UI.Consoles.MigrationConsole.Legacy.Models
{
    public class LegacyTerritory
    {
        public int TerritoryId { get; set; }
        public string TerritoryCode { get; set; }

        public string Path { get; set; }

        public bool InActive { get; set; }

        public ICollection<LegacyLedgerEntry> LedgerEntries { get; set; }
    }
}
