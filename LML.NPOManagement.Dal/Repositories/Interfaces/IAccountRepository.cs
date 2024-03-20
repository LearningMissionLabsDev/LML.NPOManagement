using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<AccountModel>> GetAllAccounts();
        Task<AccountModel> GetAccountById(int accountId);
        Task<List<AccountModel>> GetAccountsByName(string name);
        Task<List<UserModel>> GetUsersByAccount(int userId);
        Task<List<AccountUserActivityModel>> GetAccountRoleProgress(int accountId, int accountRoleId);
        Task<List<Account2UserModel>> GetAccount2Users();
        Task<AccountModel> AddAccount(AccountModel accountModel);
        Task<bool> AddUserToAccount(Account2UserModel account2UserModel);
        Task<AccountUserActivityModel> AddAccountUserActivityProgress(AccountUserActivityModel accountUserActivityModel);
        Task<AccountModel> ModifyAccount(AccountModel accountModel);     
        Task<AccountModel> RemoveUserFromAccount(int accountId, int userId);
        Task<bool> DeleteAccount(int accountId);       
        
    }
}
