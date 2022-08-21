using System;

namespace Fire.ViewModels.User
{
    public class UserViewModels
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }

       
    }
}
