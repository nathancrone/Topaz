using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class PhoneType
    {
        public PhoneType()
        {
            InaccessibleContact = new List<InaccessibleContact>();
        }
        public int PhoneTypeId { get; set; }
        public string Name { get; set; }
        public List<InaccessibleContact> InaccessibleContact { get; set; }
    }
}
