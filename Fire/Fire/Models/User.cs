﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Fire.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public virtual Person IdUserNavigation { get; set; }
    }
}
