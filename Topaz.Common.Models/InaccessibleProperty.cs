using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class InaccessibleProperty
    {
        public InaccessibleProperty()
        {
            ContactLists = new List<InaccessibleContactList>();
        }
        public int InaccessiblePropertyId { get; set; }
        public int TerritoryId { get; set; }
        public int? CurrentContactListId { get; set; }
        public DateTime? ResearchedDate { get; set; }
        public string StreetNumbers { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int EstimatedDwellingCount { get; set; }
        public string PropertyName { get; set; }
        public string Description { get; set; }
        public InaccessibleTerritory Territory { get; set; }
        public List<InaccessibleContactList> ContactLists { get; set; }
    }
}
