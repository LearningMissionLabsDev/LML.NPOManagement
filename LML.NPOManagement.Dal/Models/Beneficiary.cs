using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class Beneficiary
    {
        public Beneficiary()
        {
            AccountManagerInfos = new HashSet<AccountManagerInfo>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public int AccountManagerCategoryId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string DateOfBirth { get; set; } = null!;
        public int Gender { get; set; }
        public string? BeneficiaryInfo { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;

        public virtual ICollection<AccountManagerInfo> AccountManagerInfos { get; set; }
    }
}
