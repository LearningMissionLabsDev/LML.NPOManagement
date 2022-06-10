using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IAccountService
    {
        Task <List<AccountModel>> GetAllAccounts();
        Task<AccountModel> GetAccountById(int id);
        public int AddAccount(AccountModel accountModel);
        public int ModifyAccount(AccountModel accountModel, int id);
        public void DeleteAccount(int id);
        Task<UserIdeaModel> AddUserIdea(UserIdeaModel userIdeaModel);
        Task<List<UserIdeaModel>> GetAllIdea();
    }
}
