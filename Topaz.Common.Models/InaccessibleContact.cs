using System;
using System.Collections.Generic;
using Topaz.Common.Enums;

namespace Topaz.Common.Models
{
    public class InaccessibleContact
    {
        public InaccessibleContact()
        {
            ContactActivity = new List<InaccessibleContactActivity>();
        }
        public int InaccessibleContactId { get; set; }
        public int InaccessibleContactListId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public int? Age { get; set; }
        public string MailingAddress { get; set; }
        public int PhoneTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public InaccessibleContactList ContactList { get; set; }
        public List<InaccessibleContactActivity> ContactActivity { get; set; }
        public PhoneType PhoneType { get; set; }
        public bool IsCompleted { get; set; }
    }
}
