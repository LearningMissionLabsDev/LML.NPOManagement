using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LML.NPOManagement.Bll.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;

        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AccountModel> GetAccountById(int accountId)
        {
            if (accountId <= 0)
            {
                return null;
            }

            var accountModel = await _accountRepository.GetAccountById(accountId);
            
            if (accountModel == null)
            {
                return null;
            }

            return accountModel;
        }

        public async Task<List<AccountModel>> GetAllAccounts()
        {
            var accounts = await _accountRepository.GetAllAccounts();
            if (accounts == null)
            {
                return null;
            }

            return accounts;
        }

        public async Task<List<AccountModel>> GetAccountsByStatus(List<int>? statusIds)
        {
            var accounts = await _accountRepository.GetAccountsByStatus(statusIds);
            if (accounts == null)
            {
                return null;
            }

            return accounts;
        }

        public async Task<List<AccountModel>> GetAccountsByName(string accountName)
        {
            if (string.IsNullOrEmpty(accountName))
            {
                return null;
            }

            var accounts = await _accountRepository.GetAccountsByName(accountName);
            if (accounts == null)
            {
                return null;
            }

            return accounts;
        }

        public async Task<List<UserInformationModel>> GetUsersByAccount(int accountId)
        {
            if (accountId <= 0)
            {
                return null;
            }

            var account = await _accountRepository.GetAccountById(accountId);
            if (account == null)
            {
                return null;
            }

            var usersByAccount = await _accountRepository.GetUsersByAccount(accountId);
            if (usersByAccount == null)
            {
                return null;
            }
            return usersByAccount;
        }

        public async Task<List<AccountModel>> GetVisibleAccounts()
        {
            var accounts = await _accountRepository.GetVisibleAccounts();
            if (accounts == null)
            {
                return null;
            }

            return accounts;
        }

        public async Task<List<AccountModel>> GetAccountsByUserId(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }

            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                return null;
            }

            var accountsByUser = await _accountRepository.GetAccountsByUserId(userId);
            if (accountsByUser == null)
            {
                return null;
            }
            return accountsByUser;
        }

        public async Task<string> AccountLogin(Account2UserModel account2UserModel, UserModel userModel)
        {
            string token = TokenCreationHelper.GenerateJwtToken(userModel, _configuration, _userRepository, account2UserModel.AccountId);
            return token;
        }

        public async Task<AccountModel> AddAccount(AccountModel accountModel)
        {
            if (accountModel == null || string.IsNullOrEmpty(accountModel.Name))
            {
                return null;
            }

            var account = await _accountRepository.AddAccount(accountModel);
            if (account == null)
            {
                return null;
            }

            account.StatusId = (int)AccountStatusEnum.Active;
            return account;
        }

        public async Task<AccountModel> ModifyAccount(AccountModel accountModel)
        {
            if (accountModel == null)
            {
                return null;
            }

            var account = await _accountRepository.ModifyAccount(accountModel);
            if (account == null)
            {
                return null;
            }
            return account;
        }

        public async Task<bool> AddUserToAccount(Account2UserModel account2UserModel)
        {
            if (account2UserModel == null)
            {
                return false;
            }

            var account = await _accountRepository.GetAccountById(account2UserModel.AccountId);
            if (account == null)
            {
                return false;
            }

            var result = await _accountRepository.AddUserToAccount(account2UserModel);
            return result;
        }

        public async Task<bool> RemoveUserFromAccount(int accountId, int userId)
        {
            if (accountId <= 0 || userId <= 0)
            {
                return false;
            }

            var account = await _accountRepository.GetAccountById(accountId);
            var user = await _userRepository.GetUserById(userId);
            if (account == null || user == null)
            {
                return false;
            }

            var accountModel = await _accountRepository.RemoveUserFromAccount(accountId, userId);
            if (accountModel == null)
            {
                return false;
            }

            var deletedUser = accountModel.Account2Users.Select(us => us.User).FirstOrDefault(us => us.Id == userId);
            if (deletedUser == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAccount(int accountId)
        {
            if (accountId <= 0)
            {
                throw new ArgumentException("Account Not Valid");
            }

            var account = await _accountRepository.DeleteAccount(accountId);
            return account;
        }

        public async Task<List<AccountUserActivityModel>> GetAccountRoleProgress(int accountId, int accountRoleId)
        {
            if (accountId <= 0)
            {
                return null;
            }

            var userProgresses = await _accountRepository.GetAccountRoleProgress(accountId);
            if (userProgresses == null)
            {
                return null;
            }

            var accountUserActivities = userProgresses.Where(ben => ben.Account2UserModel.AccountRoleId == accountRoleId).ToList();
            if (accountUserActivities == null || !accountUserActivities.Any())
            {
                return null;
            }

            return accountUserActivities;
        }

        public async Task<AccountUserActivityModel> AddAccountUserActivityProgress(AccountUserActivityModel accountUserActivityModel)
        {
            var account2User = await _accountRepository.GetAccount2Users();
            if (account2User == null)
            {
                return null;
            }

            var account2UserCheck =  account2User.FirstOrDefault(acc => acc.Id == accountUserActivityModel.Account2UserId);
            if(account2UserCheck == null)
            {
                return null;
            }

            var activityUser = await _accountRepository.AddAccountUserActivityProgress(accountUserActivityModel);
            if (activityUser == null)
            {
                return null;
            }

            return activityUser;
        }
    }
}
