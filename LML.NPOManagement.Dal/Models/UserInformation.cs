using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class UserInformation
    {
        public UserInformation()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public int Gender { get; set; }
        public string? Information { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
