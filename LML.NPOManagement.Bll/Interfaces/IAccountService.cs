using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using Microsoft.Extensions.Configuration;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountModel>> GetAllAccounts();
        Task<AccountModel> GetAccountById(int accountId);
        Task<List<AccountModel>> GetAccountsByName(string name);
        Task<List<AccountUserActivityModel>> GetBeneficiariesProgress(int accountId);
        Task<List<UserModel>> GetUsersByAccount(int accountId);
        Task<AccountModel> AddAccount(AccountModel accountModel);
        Task<bool> AddUserToAccount(int accountId, int userId, int userAccountRoleEnum);
        Task<AccountModel> ModifyAccount(AccountModel accountModel, int id);
        Task<bool> RemoveUserFromAccount(int accountId, int userId);
        Task<bool> DeleteAccount(int accountId);
    }
}
