using System;
using System.Collections.Generic;

namespace Topaz.UI.MigrationConsole.Legacy.Models
{
    public class LegacyUser
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<LegacyLedgerEntry> LedgerEntries { get; set; }
    }
}
