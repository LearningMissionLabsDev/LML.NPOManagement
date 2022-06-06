using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IAccountService
    {
        public IEnumerable<AccountModel> GetAllAccounts();
        public AccountModel GetAccountById(int id);
        public int AddAccount(AccountModel accountModel);
        public int ModifyAccount(AccountModel accountModel, int id);
        public void DeleteAccount(int id);
        public UserIdeaModel AddUserIdea(UserIdeaModel userIdeaModel);
        public List<UserIdeaModel> GetAllIdea();
    }
}
