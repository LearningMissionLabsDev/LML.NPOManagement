using System.Collections.Generic;

namespace LML.NPOManagement.Bll.Model
{
    public  class AccountModel
    {
        public AccountModel()
        {
            AccountProgresses = new HashSet<AccountProgressModel>();
            Users = new HashSet<UserModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }

        public virtual ICollection<AccountProgressModel> AccountProgresses { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }
    }
}
