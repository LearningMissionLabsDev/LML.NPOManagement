using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Bll.Services
{
    public class AccountService : IAccountService
    {
        private IMapper _mapper;
        private readonly IBaseRepository _baseRepository;
        private readonly IAccountRepository _accountRepository;
      
        public AccountService(IAccountRepository accountRepository, IBaseRepository baseRepository)
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
            _baseRepository = baseRepository;
            _accountRepository = accountRepository;
        }

        public async Task <AccountModel> AddAccount(AccountModel accountModel)
        {
            var account = _mapper.Map<AccountModel, Account>(accountModel);
            await _accountRepository.Accounts.AddAsync(account);
            await _baseRepository.SaveChangesAsync();
            return accountModel;
        }

        public async Task<UserIdeaModel> AddUserIdea(UserIdeaModel userIdeaModel)
        {
            var idea =_mapper.Map<UserIdeaModel,UserIdea>(userIdeaModel);
            await _accountRepository.UserIdeas.AddAsync(idea);
            _baseRepository.SaveChanges();
            return userIdeaModel;
        }
        public async Task<List<UserIdeaModel>> GetAllIdea()
        {
            var ideas = await _accountRepository.UserIdeas.ToListAsync();
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
            var delatAccount = _accountRepository.Accounts.Where(da => da.Id == id).FirstOrDefault();
            if (delatAccount != null)
            {
                _baseRepository.SaveChanges();
            }
        }

        public async Task<AccountModel> GetAccountById(int id)
        {
            var account = await _accountRepository.Accounts.Where(acc => acc.Id == id).FirstOrDefaultAsync();
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
            var accounts = await _accountRepository.Accounts.ToListAsync();
            foreach (var account in accounts)
            {
                var accountModel = _mapper.Map<Account, AccountModel>(account);
                accountModels.Add(accountModel);
            }
            return accountModels;
        }

        public async Task <AccountModel> ModifyAccount(AccountModel accountModel, int id)
        {
            var account = await _accountRepository.Accounts.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (account == null)
            {
               return null;
            }
            account.Description = accountModel.Description;
            account.Name = accountModel.Name;
            account.Status=accountModel.Status;
            await _baseRepository.SaveChangesAsync();
            var newAccount = _mapper.Map<Account, AccountModel>(account);
            return newAccount;
        }

       
    }
}
