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
        Task<List<AccountUserActivityModel>> GetBeneficiariesProgress(int accountId);
        Task<AccountModel> AddAccount(AccountModel accountModel);
        Task<bool> AddUserToAccount(int accountId, int userId, int userAccountRole);
        Task<AccountModel> ModifyAccount(AccountModel accountModel, int id);     
        Task<AccountModel> RemoveUserFromAccount(int accountId, int userId);
        Task<bool> DeleteAccount(int accountId);       
        
    }
}
