using System;
using System.Collections.Generic;

namespace Topaz.UI.Consoles.MigrationConsole.Legacy.Models
{
    public class LegacyLedgerEntry
    {
        public int LedgerEntryId { get; set; }

        public int TerritoryId { get; set; }

        public string UserId { get; set; }

        public DateTime? CheckOutDate { get; set; }

        public DateTime? CheckInDate { get; set; }

        public LegacyTerritory Territory { get; set; }

        public LegacyUser User { get; set; }
    }
}
