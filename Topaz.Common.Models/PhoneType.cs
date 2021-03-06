﻿using System;
using System.Collections.Generic;

namespace Topaz.Common.Models
{
    public class PhoneType
    {
        public PhoneType()
        {
            InaccessibleContacts = new List<InaccessibleContact>();
        }
        public int PhoneTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<InaccessibleContact> InaccessibleContacts { get; set; }
    }
}
