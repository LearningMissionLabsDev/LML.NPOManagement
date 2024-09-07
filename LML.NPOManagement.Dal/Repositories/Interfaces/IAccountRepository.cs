using LML.NPOManagement.Common.Model;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<AccountModel>> GetAllAccounts();
        Task<List<AccountModel>> GetVisibleAccounts();
        Task<List<AccountModel>> GetAccountsByStatus(List<int>? statusIds);
        Task<AccountModel> GetAccountById(int accountId);
        Task<List<AccountModel>> GetAccountsByName(string name);
        Task<List<UserInformationModel>> GetUsersByAccount(int userId);
        Task<List<AccountUserActivityModel>> GetAccountRoleProgress(int accountId);
        Task<List<AccountModel>> GetAccountsByUserId(int userId);
        Task<List<Account2UserModel>> GetAccount2Users();
        Task<AccountModel> AddAccount(AccountModel accountModel); 
        Task<bool> AddUserToAccount(Account2UserModel account2UserModel);
        Task<AccountUserActivityModel> AddAccountUserActivityProgress(AccountUserActivityModel accountUserActivityModel);
        Task<AccountModel> ModifyAccount(AccountModel accountModel);     
        Task<AccountModel> RemoveUserFromAccount(int accountId, int userId);
        Task<bool> DeleteAccount(int accountId);
    }
}
