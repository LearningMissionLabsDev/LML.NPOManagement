using LML.NPOManagement.Dal.Models;
using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Bll.Model
{
    public class AccountModel
    {
        public AccountModel()
        {
            AccountProgresses = new HashSet<AccountProgressModel>();
            Users = new HashSet<UserModel>();
        }

        public int Id { get; set; }
        public string AccountName { get; set; } = null!;
        public string AccountDescription { get; set; }

        public virtual ICollection<AccountProgressModel> AccountProgresses { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }
    }
}
