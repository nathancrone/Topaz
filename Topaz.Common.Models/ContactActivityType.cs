using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class ContactActivityType
    {
        public ContactActivityType()
        {
            ContactActivity = new List<InaccessibleContactActivity>();
        }

        public int ContactActivityTypeId { get; set; }
        public string Name { get; set; }
        public List<InaccessibleContactActivity> ContactActivity { get; set; }
    }
}
