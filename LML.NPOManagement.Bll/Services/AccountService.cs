﻿using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

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

        public async Task<List<UserModel>> GetUsersByAccount(int accountId)
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

        public async Task<Account2UserModel> AccountLogin(Account2UserModel account2UserModel)
        {

            var account = await _userRepository.GetUsersInfoAccount(account2UserModel.UserId);

            if (account == null)
            {
                return null;
            }
            var account2user = account.FirstOrDefault(ac => ac.AccountId == account2UserModel.AccountId);

            if (account2user == null)
            {
                return null;
            }
            return account2user;
        }

        public async Task<AccountModel> AddAccount(AccountModel accountModel)
        {
            if (accountModel == null || string.IsNullOrEmpty(accountModel.Name))
            {
                return null;
            }
            accountModel.StatusId = (int)AccountStatusEnum.Active;
            var account = await _accountRepository.AddAccount(accountModel);

            if (account == null)
            {
                return null;
            }
            return account;
        }

        public async Task<AccountModel> ModifyAccount(AccountModel accountModel, int accountId)
        {
            if (accountModel == null || accountId <= 0)
            {
                return null;
            }
            var account = await _accountRepository.ModifyAccount(accountModel, accountId);

            if (account == null)
            {
                return null;
            }
            return account;
        }

        public async Task<bool> AddUserToAccount(int accountId, int userId, int userAccountRoleEnum)
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
            var result = await _accountRepository.AddUserToAccount(accountId, userId, userAccountRoleEnum);

            if (!result)
            {
                return false;
            }
            return true;
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

            if (!account)
            {
                return false;
            }
            return true;
        }

        public async Task<List<AccountUserActivityModel>> GetAccountRoleProgress(int accountId, int accountRoleId)
        {
            if (accountId <= 0)
            {
                return null;
            }
            var userProgresses = await _accountRepository.GetAccountRoleProgress(accountId, accountRoleId);

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
    }
}
