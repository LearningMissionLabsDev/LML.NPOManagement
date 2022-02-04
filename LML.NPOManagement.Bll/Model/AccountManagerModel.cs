﻿using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class AccountManagerModel
    {
        public AccountManagerModel()
        {
            AccountManagerInfos = new HashSet<AccountManagerInfo>();
        }

        public int Id { get; set; }
        public string AccountManagerCategory { get; set; } = null!;
        public string? NarrowProfessional { get; set; }

        public virtual ICollection<AccountManagerInfo> AccountManagerInfos { get; set; }
    }
}
