using System;
using System.Collections.Generic;

#nullable disable

namespace Fire.Models
{
    public partial class Admin
    {
        public int IdAdmin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public virtual Person IdAdminNavigation { get; set; }
    }
}
