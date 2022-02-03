using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class AccountManager
    {
        public AccountManager()
        {
            AccountManagerInfos = new HashSet<AccountManagerInfo>();
        }

        public int Id { get; set; }
        public string AccountManagerCategory { get; set; } = null!;
        public string? NarrowProfessional { get; set; }

        public virtual ICollection<AccountManagerInfo> AccountManagerInfos { get; set; }
    }
}
