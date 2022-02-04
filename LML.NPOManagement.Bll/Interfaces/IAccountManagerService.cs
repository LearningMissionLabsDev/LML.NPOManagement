using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IAccountManagerService
    {
        public IEnumerable<AccountManagerModel> GetAllAccountManagers();
        public AccountManagerModel GetAccountManagerById(int id);
        public int AddAccountManager(AccountManagerModel accountManagerModel);
        public int ModifyAccountManager(AccountManagerModel accountManagerModel, int id);
        public void DeleteAccountManager(int id);
    }
}
