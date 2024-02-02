using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Bll.Services
{
    public class AccountService : IAccountService
    {
        private IMapper _mapper;
        //private readonly IAccountRepository _accountRepository;
        private readonly NpomanagementContext _dbContext;

        public AccountService(/*IAccountRepository accountRepository*/)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountProgress, AccountProgressModel>();
                cfg.CreateMap<Attachment, AttachmentModel>();
                cfg.CreateMap<Donation, DonationModel>();
                cfg.CreateMap<Account, AccountModel>();
                cfg.CreateMap<InvestorInformation, InvestorInformationModel>();
                cfg.CreateMap<InventoryType, InventoryTypeModel>();
                cfg.CreateMap<Notification, NotificationModel>();
                cfg.CreateMap<UserInformation, UserInformationModel>();
                cfg.CreateMap<UserInventory, UserInventoryModel>();
                cfg.CreateMap<UserType, UserTypeModel>();
                cfg.CreateMap<AccountProgressModel, AccountProgress>();
                cfg.CreateMap<AttachmentModel, Attachment>();
                cfg.CreateMap<DonationModel, Donation>();
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformation>();
                cfg.CreateMap<InventoryTypeModel, InventoryType>();
                cfg.CreateMap<NotificationModel, Notification>();
                cfg.CreateMap<UserInformationModel, UserInformation>();
                cfg.CreateMap<UserInventoryModel, UserInventory>();
                cfg.CreateMap<UserTypeModel, UserType>();
                cfg.CreateMap<UserIdeaModel, UserIdea>();
                cfg.CreateMap<UserIdea, UserIdeaModel>();
            });
            _mapper = config.CreateMapper();
            //_accountRepository = accountRepository;
        }

        public async Task <AccountModel> AddAccount(AccountModel accountModel)
        {
            var account = _mapper.Map<AccountModel, Account>(accountModel);
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
            return accountModel;
        }

        public async Task<UserIdeaModel> AddUserIdea(UserIdeaModel userIdeaModel)
        {
            var idea =_mapper.Map<UserIdeaModel,UserIdea>(userIdeaModel);
            await _dbContext.UserIdeas.AddAsync(idea);
            _dbContext.SaveChanges();
            return userIdeaModel;
        }
        public async Task<List<UserIdeaModel>> GetAllIdea()
        {
            var ideas = await _dbContext.UserIdeas.ToListAsync();
            if(ideas.Count == 0)
            {
                return null;
            }
            List<UserIdeaModel> userIdeaModels = new List<UserIdeaModel>();
            foreach (var idea in ideas)
            {
                var ideaModel = _mapper.Map<UserIdea, UserIdeaModel>(idea);
                userIdeaModels.Add(ideaModel);
            }
            return userIdeaModels;
        }

        public void DeleteAccount(int id)
        {
            var delatAccount = _dbContext.Accounts.Where(da => da.Id == id).FirstOrDefault();
            if (delatAccount != null)
            {
                _dbContext.SaveChanges();
            }
        }

        public async Task<AccountModel> GetAccountById(int id)
        {
            var account = await _dbContext.Accounts.Where(acc => acc.Id == id).FirstOrDefaultAsync();
            if (account != null)
            {
                var accountModel = _mapper.Map<Account, AccountModel>(account);
                return accountModel;
            }
            return null;          
        }

        public async Task<List<AccountModel>> GetAllAccounts()
        {
            List<AccountModel> accountModels = new List<AccountModel>();
            var accounts = await _dbContext.Accounts.ToListAsync();
            foreach (var account in accounts)
            {
                var accountModel = _mapper.Map<Account, AccountModel>(account);
                accountModels.Add(accountModel);
            }
            return accountModels;
        }
        public async Task <AccountModel> ModifyAccount(AccountModel accountModel, int id)
        {
            var account = await _dbContext.Accounts.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (account == null)
            {
               return null;
            }
            account.Description = accountModel.Description;
            account.Name = accountModel.Name;
            //account.Status=accountModel.Status;
            await _dbContext.SaveChangesAsync();
            var newAccount = _mapper.Map<Account, AccountModel>(account);
            return newAccount;
        }
    }
}
