﻿using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using Microsoft.Extensions.Configuration;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountModel>> GetAllAccounts();
        Task<AccountModel> GetAccountById(int id);
        Task<List<AccountModel>> GetAccountsByName(string name);
        Task<List<UserModel>> GetUsersByAccount(int id);
        Task<Account2UserModel> AccountLogin(Account2UserModel account2UserModel, IConfiguration configuration);
        Task<AccountModel> AddAccount(AccountModel accountModel);
        Task<bool> AddUserToAccount(int accountId, int userId, int userAccountRoleEnum);
        Task<AccountModel> ModifyAccount(AccountModel accountModel, int id);
        Task<bool> RemoveUserFromAccount(int accountId, int userId);
        Task<bool> DeleteAccount(int accountId);
    }
}
