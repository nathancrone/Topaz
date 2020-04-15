using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class InaccessibleContactList
    {
        public int InaccessibleContactListId { get; set; }
        public int InaccessibleAddressId { get; set; }
        public DateTime? CreateDate { get; set; }
        public InaccessibleAddress Address { get; set; }
        public List<InaccessibleContact> Contacts { get; set; }
    }
}
