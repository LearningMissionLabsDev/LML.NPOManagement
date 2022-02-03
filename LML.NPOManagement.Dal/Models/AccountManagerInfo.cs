using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class AccountManagerInfo
    {
        public AccountManagerInfo()
        {
            Beneficiries = new HashSet<Beneficiary>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public int AccountManagerCategoryId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? Information { get; set; }

        public virtual AccountManager AccountManagerCategory { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;

        public virtual ICollection<Beneficiary> Beneficiries { get; set; }
    }
}
