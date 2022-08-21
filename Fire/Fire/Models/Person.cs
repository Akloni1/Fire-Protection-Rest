using System;
using System.Collections.Generic;

#nullable disable

namespace Fire.Models
{
    public partial class Person
    {
        public int IdPerson { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual User User { get; set; }
    }
}
