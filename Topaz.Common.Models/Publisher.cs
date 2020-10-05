using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class Publisher
    {
        public Publisher()
        {
            Activity = new List<TerritoryActivity>();
            InaccessibleContacts = new List<InaccessibleContact>();
            InaccessibleContactActivity = new List<InaccessibleContactActivity>();
        }
        public int PublisherId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsHidden { get; set; }
        public List<TerritoryActivity> Activity { get; set; }
        public List<InaccessibleContact> InaccessibleContacts { get; set; }
        public List<InaccessibleContactActivity> InaccessibleContactActivity { get; set; }
        public List<DoNotContactStreet> StreetDoNotContacts { get; set; }
        public List<DoNotContactLetter> LetterDoNotContacts { get; set; }
        public List<DoNotContactPhone> PhoneDoNotContacts { get; set; }
    }
}
