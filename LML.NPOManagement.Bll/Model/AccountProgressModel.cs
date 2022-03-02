using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class AccountProgressModel
    {
        /// <summary>
        /// From Progress in account
        /// </summary>
        public AccountProgressModel(AccountProgress accountProgress)
        {
            accountProgress.Id = Id;
            accountProgress.AccountId = AccountId;
            accountProgress.Description = Description;
        }
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; } 

        public virtual AccountModel Account { get; set; } 
    }
}
