using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class Publisher
    {
        public Publisher()
        {
            Activity = new List<TerritoryActivity>();
        }
        public int PublisherId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<TerritoryActivity> Activity { get; set; }
        public List<InaccessibleContact> InaccessibleContacts { get; set; }
        public List<InaccessibleContactActivity> InaccessibleContactActivity { get; set; }
    }
}
