using AutoMapper;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Dal.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;
        private readonly NpomanagementContext _dbContext;
        
        public AccountRepository(NpomanagementContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<Account, AccountModel>();
                cfg.CreateMap<Account2User, Account2UserModel>();
                cfg.CreateMap<Account2UserModel, Account2User>();
                cfg.CreateMap<AccountUserActivityModel, AccountUserActivity>();
                cfg.CreateMap<AccountUserActivity, AccountUserActivityModel>();
                cfg.CreateMap<User, UserModel>();
            });
            _mapper = config.CreateMapper();
            _dbContext = context;
        }

        public async Task<AccountModel> GetAccountById(int accountId)
        {
            if (accountId <= 0)
            {
                return null;
            }

            var account = await _dbContext.Accounts.Where(acc => acc.Id == accountId).FirstOrDefaultAsync();
            if (account == null)
            {
                return null;
            }

            return _mapper.Map<AccountModel>(account);
        }

        public async Task<List<AccountModel>> GetAllAccounts()
        {
            var accounts = await _dbContext.Accounts.ToListAsync();
            if (accounts.Count < 1)
            {
                return null;
            }

            var accountsModel = new List<AccountModel>();
            foreach (var account in accounts)
            {
                var accountModel = new AccountModel
                {
                    Id = account.Id,
                    Name = account.Name,
                    MaxCapacity = account.MaxCapacity,
                    CreatorId = account.CreatorId,
                    StatusId = account.StatusId,
                    DateCreated = account.DateCreated,
                    IsVisible = account.IsVisible,
                    OnboardingLink = account.OnboardingLink,
                    Description = account.Description,
                    AccountImage = account.AccountImage,
                    DeletedAt = account.DeletedAt
                };
                accountsModel.Add(accountModel);
            }

            return accountsModel;
        }


        public async Task<List<AccountModel>> GetVisibleAccounts()
        {
            var accounts = await _dbContext.Accounts.Where(acc => acc.IsVisible == true).ToListAsync();
            if (accounts.Count < 1)
            {
                return null;
            }

            var accountsModel = new List<AccountModel>();
            foreach (var account in accounts)
            {
                var accountModel = new AccountModel
                {
                    Id = account.Id,
                    Name = account.Name,
                    MaxCapacity = account.MaxCapacity,
                    CreatorId = account.CreatorId,
                    StatusId = account.StatusId,
                    DateCreated = account.DateCreated,
                    IsVisible = account.IsVisible,
                    OnboardingLink = account.OnboardingLink,
                    Description = account.Description,
                    AccountImage = account.AccountImage,
                    DeletedAt = account.DeletedAt
                };

                accountsModel.Add(accountModel);
            }

            return accountsModel;
        }

        public async Task<List<AccountModel>> GetAccountsByStatus(List<int>? statusIds)
        {
            var query = _dbContext.Accounts.AsQueryable();
            if (statusIds != null && statusIds.Any())
            {
                query = query.Where(u => statusIds.Contains(u.StatusId));
            }

            var accounts = await query.ToListAsync();
            if (!accounts.Any())
            {
                return null;
            }

            return _mapper.Map<List<AccountModel>>(accounts);
        }

        public async Task<List<UserInformationModel>> GetUsersByAccount(int accountId)
        {
            if (accountId <= 0)
            {
                return null;
            }

            var account2Users = await _dbContext.Account2Users.Where(acc2User => acc2User.AccountId == accountId).Include(acc2User => acc2User.User).ToListAsync();

            var usersInformationModel = new List<UserInformationModel>();
            foreach (var account in account2Users) {
                var userInformation = await _dbContext.UserInformations.Where(userInformation => userInformation.UserId == account.User.Id).FirstOrDefaultAsync();

                if (userInformation == null) continue;

                var userInfo = new UserInformationModel
                {
                    Id = userInformation.UserId,
                    FirstName = userInformation.FirstName,
                    LastName = userInformation.LastName,
                    UserImage = userInformation.UserImage,
                    AccountRoleId = account.AccountRoleId
                };

                usersInformationModel.Add(userInfo);
            }

            return usersInformationModel;
        }

        public async Task<List<AccountModel>> GetAccountsByUserId(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }

            var user = await _dbContext.Users.Include(acc2us => acc2us.Account2Users).ThenInclude(us => us.Account).Where(us => us.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }

            var accounts = user.Account2Users.Select(au => au.Account).ToList();
            if (accounts.Count < 1)
            {
                return null;
            }

            var accountModel = new List<AccountModel>();
            foreach (var account in accounts)
            {
                var accountInfo = new AccountModel
                {
                    Id = account.Id,
                    Name = account.Name,
                    Description = account.Description,
                    AccountImage = account.AccountImage
                };
                accountModel.Add(accountInfo);
            }

            return accountModel;
        }


        public async Task<List<Account2UserModel>> GetAccount2Users()
        {
            var account2users = await _dbContext.Account2Users.ToListAsync();
            if (account2users == null || !account2users.Any())
            {
                return null;
            }

            var account2usermodel = _mapper.Map<List<Account2UserModel>>(account2users);
            return account2usermodel;
        }

        public async Task<AccountModel> AddAccount(AccountModel accountModel)
        {
            if (accountModel == null || string.IsNullOrEmpty(accountModel.Name))
            {
                return null;
            }

            var account = new Account()
            {
                Id = accountModel.Id,
                IsVisible = accountModel.IsVisible,
                StatusId = accountModel.StatusId,
                CreatorId = accountModel.CreatorId,
                MaxCapacity = accountModel.MaxCapacity,
                Name = accountModel.Name,
                OnboardingLink = accountModel.OnboardingLink,
                Description = accountModel.Description,
                AccountImage = accountModel.AccountImage
            };

            account.StatusId = (int)AccountStatusEnum.Active;
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();

            var newAccount = await _dbContext.Accounts
                    .Include(acc2us => acc2us.Account2Users)
                    .Where(acc => acc.Id == account.Id)
                    .FirstOrDefaultAsync();

            if (newAccount == null)
            {
                return null;
            }

            var creatorUser = new Account2User()
            {
                UserId = newAccount.CreatorId,
                AccountRoleId = (int)UserAccountRoleEnum.Admin
            };

            newAccount.Account2Users.Add(creatorUser);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<AccountModel>(newAccount);
        }

        public async Task<AccountModel> ModifyAccount(AccountModel accountModel)
        {

            if (accountModel == null)
            {
                return null;
            }

            var account = await _dbContext.Accounts.Where(acc => acc.Id == accountModel.Id).FirstOrDefaultAsync();
            if (account == null)
            {
                return null;
            }

            if (accountModel.StatusId == (int)AccountStatusEnum.Deleted)
            {
                return null;
            }

            account.Name = accountModel.Name;
            account.IsVisible = accountModel.IsVisible;
            account.MaxCapacity = accountModel.MaxCapacity;
            account.OnboardingLink = accountModel.OnboardingLink;
            account.Description = accountModel.Description;
            account.StatusId = accountModel.StatusId;
            account.AccountImage = accountModel.AccountImage;

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<AccountModel>(account);
        }

        public async Task<bool> DeleteAccount(int accountId)
        {
            if (accountId <= 0)
            {
                return false;
            }

            var account = await _dbContext.Accounts
                .Include(us => us.Account2Users)
                .ThenInclude(acc => acc.AccountUserActivities)
                .FirstOrDefaultAsync(acc => acc.Id == accountId);

            if (account == null)
            {
                return false;
            }

            var userActivities = account.Account2Users.SelectMany(act => act.AccountUserActivities).ToList();
            _dbContext.AccountUserActivities.RemoveRange(userActivities);
            await _dbContext.SaveChangesAsync();

            _dbContext.Account2Users.RemoveRange(account.Account2Users);
            await _dbContext.SaveChangesAsync();

            account.StatusId = (int)AccountStatusEnum.Deleted;
            account.DeletedAt = DateTime.Now;
            account.IsVisible = false;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<AccountModel>> GetAccountsByName(string accountName)
        {
            if (string.IsNullOrEmpty(accountName))
            {
                return null;
            }

            var accounts = await _dbContext.Accounts.Where(acc => acc.Name.Contains(accountName)).ToListAsync();
            if (accounts.Count < 1)
            {
                return null;
            }

            return _mapper.Map<List<AccountModel>>(accounts);
        }

        public async Task<bool> AddUserToAccount(Account2UserModel account2UserModel)
        {
            if (account2UserModel == null)
            {
                return false;
            }

            var account = await _dbContext.Accounts.Include(acc => acc.Account2Users).FirstOrDefaultAsync(acc => acc.Id == account2UserModel.AccountId);
            var user = await _dbContext.Users.FirstOrDefaultAsync(us => us.Id == account2UserModel.UserId);
            if (account == null || user == null)
            {
                return false;
            }

            var availableUser = account.Account2Users.FirstOrDefault(us => us.UserId == user.Id);
            if (availableUser == null)
            {
                var accountUser = new Account2User()
                {
                    AccountId = account2UserModel.AccountId,
                    UserId = account2UserModel.UserId,
                    AccountRoleId = account2UserModel.AccountRoleId
                };
                account.Account2Users.Add(accountUser);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<AccountModel> RemoveUserFromAccount(int accountId, int userId)
        {
            if (accountId <= 0 || userId <= 0)
            {
                return null;
            }

            var account2user = await _dbContext.Account2Users.Include(act => act.AccountUserActivities).FirstOrDefaultAsync(_ => _.AccountId == accountId && _.UserId == userId);
            if (account2user == null)
            {
                return null;
            }

            _dbContext.AccountUserActivities.RemoveRange(account2user.AccountUserActivities);
            await _dbContext.SaveChangesAsync();

            _dbContext.Account2Users.Remove(account2user);
            await _dbContext.SaveChangesAsync();

            var account = await _dbContext.Accounts.Include(u => u.Account2Users).ThenInclude(u => u.User).FirstOrDefaultAsync(acc => acc.Id == accountId);
            var newAccountModel = new AccountModel
            {
                Id = account.Id,
                CreatorId = account.CreatorId,
                StatusId = account.StatusId,
                IsVisible = account.IsVisible,
                DateCreated = account.DateCreated,
                Name = account.Name,
                MaxCapacity = account.MaxCapacity,
                OnboardingLink = account.OnboardingLink,
                Description = account.Description
            };

            return newAccountModel;
        }

        public async Task<List<AccountUserActivityModel>> GetAccountRoleProgress(int accountId)
        {
            if (accountId <= 0)
            {
                return null;
            }

            var userProgress = await _dbContext.AccountUserActivities.Where(acc => acc.Account2User.AccountId == accountId).Include(acc2user => acc2user.Account2User).ToListAsync();
            if (userProgress.Count < 1)
            {
                return null;
            }
            
            var activityMapping = userProgress.Select(map => new AccountUserActivityModel
            {
                Account2UserId = map.Account2UserId,
                Account2UserModel = _mapper.Map<Account2UserModel>(map.Account2User),
                ActivityInfo = map.ActivityInfo,
                DateCreated = map.DateCreated,
                Id = map.Id
            }).ToList();

            return activityMapping;
        }

        public async Task<AccountUserActivityModel> AddAccountUserActivityProgress(AccountUserActivityModel accountUserActivityModel)
        {
            var userActivity = new AccountUserActivity()
            {
                Id = accountUserActivityModel.Id,
                Account2UserId = accountUserActivityModel.Account2UserId,
                DateCreated = accountUserActivityModel.DateCreated,
                ActivityInfo = accountUserActivityModel.ActivityInfo
            };

            await _dbContext.AccountUserActivities.AddAsync(userActivity);
            await _dbContext.SaveChangesAsync();
            var activity = await _dbContext.AccountUserActivities.FirstOrDefaultAsync(act => act.Id == userActivity.Id);
            if (activity == null) 
            { 
                return null; 
            }

            var userActivityModel = new AccountUserActivityModel()
            {
                Id = activity.Id,
                Account2UserId = activity.Account2UserId,
                ActivityInfo = activity.ActivityInfo,
                DateCreated = activity.DateCreated
            };

            return userActivityModel;
        }
    }
}