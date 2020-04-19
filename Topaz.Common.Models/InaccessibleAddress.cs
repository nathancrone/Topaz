using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class InaccessibleAddress
    {
        public InaccessibleAddress()
        {
            ContactLists = new List<InaccessibleContactList>();
        }
        public int InaccessibleAddressId { get; set; }
        public int TerritoryId { get; set; }
        public int? CurrentContactListId { get; set; }
        public DateTime? ResearchedDate { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int EstimatedDwellingCount { get; set; }
        public string PropertyName { get; set; }
        public string Notes { get; set; }
        public InaccessibleTerritory Territory { get; set; }
        public List<InaccessibleContactList> ContactLists { get; set; }
    }
}
