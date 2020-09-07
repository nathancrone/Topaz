using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class PhoneResponseType
    {
        public PhoneResponseType()
        {
            ContactActivity = new List<InaccessibleContactActivity>();
        }

        public int PhoneResponseTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<InaccessibleContactActivity> ContactActivity { get; set; }
    }
}
