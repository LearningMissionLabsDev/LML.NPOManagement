using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountModel>> GetAllAccounts();
        Task<AccountModel> GetAccountById(int accountId);
        Task<List<AccountModel>> GetAccountsByName(string name);
        Task<List<AccountUserActivityModel>> GetAccountRoleProgress(int accountId, int accountRoleId);
        Task<List<UserModel>> GetUsersByAccount(int accountId);
        Task<Account2UserModel> AccountLogin(Account2UserModel account2UserModel);
        Task<AccountModel> AddAccount(AccountModel accountModel);
        Task<bool> AddUserToAccount(Account2UserModel account2UserModel);
        Task<AccountUserActivityModel> AddAccountUserActivityProgress(AccountUserActivityModel accountUserActivityModel);
        Task<AccountModel> ModifyAccount(AccountModel accountModel);
        Task<bool> RemoveUserFromAccount(int accountId, int userId);
        Task<bool> DeleteAccount(int accountId);
    }
}
