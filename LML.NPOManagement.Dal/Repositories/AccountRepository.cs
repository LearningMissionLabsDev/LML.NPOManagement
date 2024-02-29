using AutoMapper;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LML.NPOManagement.Dal.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        IMapper _mapper;
        NpomanagementContext _dbContext;
        public AccountRepository(NpomanagementContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<Account, AccountModel>();
                cfg.CreateMap<Account2User, Account2UserModel>();
                cfg.CreateMap<Account2UserModel, Account2User>();
                cfg.CreateMap<User,UserModel>();
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
            return _mapper.Map<List<AccountModel>>(accounts);
        }

        public async Task<List<UserModel>> GetUsersByAccount(int accountId)
        {
            if (accountId <= 0)
            {
                return null;
            }
            var account = await _dbContext.Accounts.Include(acc2us => acc2us.Account2Users).ThenInclude(us => us.User).Where(acc => acc.Id == accountId).FirstOrDefaultAsync();

            if (account == null)
            {
                return null;
            }
            var users = account.Account2Users.Select(au => au.User).ToList();

            if (users.Count < 1)
            {
                return null;
            }
            foreach(var user in users)
            {
                user.Password = null;
            }
            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<AccountModel> AddAccount(AccountModel accountModel)
        {
            if (accountModel == null || string.IsNullOrEmpty(accountModel.Name))
            {
                return null;
            }
            var account = _mapper.Map<Account>(accountModel);
            
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();

            var newAccount = await _dbContext.Accounts.Include(acc2us => acc2us.Account2Users).Where(acc => acc.Id == account.Id).FirstOrDefaultAsync();
           
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

        public async Task<AccountModel> ModifyAccount(AccountModel accountModel, int accountId)
        {
            if (accountModel == null || accountId <= 0)
            {
                return null;
            }
            var account = await _dbContext.Accounts.Where(acc => acc.Id == accountId).FirstOrDefaultAsync();

            if (account == null)
            {
                return null;
            }
                    
            if (accountModel.StatusId == (int)AccountStatusEnum.Deleted)
            {
                return null;
            }
            account.Name = accountModel.Name;
            account.Description = accountModel.Description;
            account.StatusId = accountModel.StatusId;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<AccountModel>(account);
        }

        public async Task<bool> DeleteAccount(int accountId)
        {
            if (accountId <= 0)
            {
                return false;
            }
            var account = await _dbContext.Accounts.Include(us => us.Account2Users).ThenInclude(u => u.User).FirstOrDefaultAsync(acc => acc.Id == accountId);

            if (account == null)
            {
                return false;
            }
            account.Account2Users.Clear();
            await _dbContext.SaveChangesAsync();

            account.StatusId = (int)AccountStatusEnum.Deleted;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<AccountModel>> GetAccountsByName(string accountName)
        {
            if(string.IsNullOrEmpty(accountName))
            {
                return null;
            }
            var accounts = await _dbContext.Accounts.Where(acc => acc.Name.Contains(accountName)).ToListAsync();

            if(accounts.Count < 1)
            {
                return null;
            }
            return _mapper.Map<List<AccountModel>>(accounts);
        }

        public async Task<bool> AddUserToAccount(int accountId, int userId, int userAccountRole)
        {
            if(accountId <= 0 || userId <= 0)
            {
                return false;
            }
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(acc => acc.Id == accountId);           
            var user = await _dbContext.Users.FirstOrDefaultAsync(us => us.Id == userId);

            if (account == null || user == null)
            {
                return false;
            }
            var availableUser = await _dbContext.Account2Users.FirstOrDefaultAsync(us => us.UserId == user.Id);
            // Update User Role
            if (availableUser == null)
            {
                var accountUser = new Account2User()
                {
                    UserId = userId,
                    AccountId = accountId,
                    AccountRoleId = userAccountRole
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
            var account = await _dbContext.Accounts.Include(acc2us => acc2us.Account2Users).ThenInclude(u => u.User).FirstOrDefaultAsync(acc => acc.Id == accountId);
            if (account == null)
            {
                return null;
            }
            var userToRemove = account.Account2Users.FirstOrDefault(us => us.UserId == userId);

            if (userToRemove == null)
            {
                return null;
            }
            account.Account2Users.Remove(userToRemove);
            await _dbContext.SaveChangesAsync();

            account = await _dbContext.Accounts.Include(u => u.Account2Users).ThenInclude(u => u.User).FirstOrDefaultAsync(acc => acc.Id == accountId);

            return _mapper.Map<AccountModel>(account);
        }

        public async Task<List<Account2UserModel>> GetUsersInfoInAccount(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }
            var account2user = await _dbContext.Account2Users.Where(us => us.UserId == userId).ToListAsync();

            if(!account2user.Any())
            {
                return null;
            }
            return _mapper.Map<List<Account2UserModel>>(account2user);
        }
    }
}
