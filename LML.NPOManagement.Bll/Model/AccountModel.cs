﻿

namespace LML.NPOManagement.Bll.Model
{
    public partial class AccountModel
    {
        public AccountModel()
        {
            AccountProgresses = new HashSet<AccountProgressModel>();
            Users = new HashSet<UserModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Status { get; set; }
        public virtual ICollection<AccountProgressModel> AccountProgresses { get; set; }
        public virtual ICollection<UserModel> Users { get; set; }
    }
}
