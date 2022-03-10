using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Services
{
    public class AccountService : IAccountService
    {
        private IMapper _mapper;
        public AccountService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountProgress, AccountProgressModel>();
                cfg.CreateMap<Account, AccountModel>();
                cfg.CreateMap<InvestorInformation, InvestorInformationModel>();               
                cfg.CreateMap<AccountProgressModel, AccountProgress>();               
                cfg.CreateMap<AccountModel, Account>();                
            });
            _mapper = config.CreateMapper();
        }

        public int AddAccount(AccountModel accountModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccount(int id)
        {
            using(var dbContext = new NPOManagementContext())
            {
                var delatAccount = dbContext.Accounts.Where(da => da.Id == id).FirstOrDefault();
                if (delatAccount != null)
                {
                    dbContext.SaveChanges();
                }
            }
        }

        public AccountModel GetAccountById(int id)
        {
            using(var dbContext = new NPOManagementContext())
            {
                var account = dbContext.Accounts.Where(acc => acc.Id == id).FirstOrDefault();
                if (account != null)
                {
                    var accountModel = _mapper.Map<Account,AccountModel>(account);
                    return accountModel;
                }
                return null;
            }
        }

        public IEnumerable<AccountModel> GetAllAccounts()
        {
            using (var dbContex = new NPOManagementContext())
            {
                var accounts = dbContex.Accounts.ToList();
                
                foreach (var account in accounts)
                {
                    var accountModel = _mapper.Map<Account, AccountModel>(account);
                    yield return accountModel;
                }
            }
        }

        public int ModifyAccount(AccountModel accountModel, int id)
        {
            throw new NotImplementedException();
        }

      
    }
}
