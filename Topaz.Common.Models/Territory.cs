using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public abstract class Territory
    {
        public Territory()
        {
            //Activity = new List<TerritoryActivity>();
        }

        public int TerritoryId { get; set; }
        public string TerritoryCode { get; set; }
        public string Notes { get; set; }
        public bool InActive { get; set; }
        //public List<TerritoryActivity> Activity { get; set; }
    }
}
