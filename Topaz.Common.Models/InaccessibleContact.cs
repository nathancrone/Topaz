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
        public string MailingAddress1 { get; set; }
        public string MailingAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int? PhoneTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddresses { get; set; }
        public int? AssignPublisherId { get; set; }
        public DateTime? AssignDate { get; set; }
        public int? AssignContactActivityTypeId { get; set; }
        public bool DoNotContactPhone { get; set; }
        public bool DoNotContactLetter { get; set; }
        public InaccessibleContactList ContactList { get; set; }
        public List<InaccessibleContactActivity> ContactActivity { get; set; }
        public PhoneType PhoneType { get; set; }
        public Publisher AssignPublisher { get; set; }
        public List<InaccessibleTerritoryExportItem> ExportItems { get; set; }
    }
}
