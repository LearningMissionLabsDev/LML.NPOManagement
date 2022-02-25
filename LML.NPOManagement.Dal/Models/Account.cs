using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class Account
    {
        public Account()
        {
            AccountProgresses = new HashSet<AccountProgress>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string AccountName { get; set; } = null!;
        public string? AccountDescription { get; set; }

        public virtual ICollection<AccountProgress> AccountProgresses { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
