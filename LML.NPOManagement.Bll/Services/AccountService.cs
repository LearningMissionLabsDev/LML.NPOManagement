using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Bll.Services
{
    public class AccountService : IAccountService
    {
        private IMapper _mapper;
        public AccountService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountManagerInfo, AccountManagerInfoModel>();
                cfg.CreateMap<Beneficiary, BeneficiaryModel>();
                cfg.CreateMap<BeneficiaryRole, BeneficiaryRoleModel>();
                cfg.CreateMap<Status, StatusModel>();
                cfg.CreateMap<Account, AccountModel>();
                cfg.CreateMap<AccountManagerRole, AccountManagerRoleModel>();
                cfg.CreateMap<InventoryType, InventoryTypeModel>();
                cfg.CreateMap<BeneficiaryInventory, BeneficiaryInventoryModel>();
                cfg.CreateMap<AccountManagerInventory, AccountManagerInventoryModel>();
                cfg.CreateMap<AccountManagerInfoModel, AccountManagerInfo>();
                cfg.CreateMap<BeneficiaryModel, Beneficiary>();
                cfg.CreateMap<BeneficiaryRoleModel, BeneficiaryRole>();
                cfg.CreateMap<StatusModel, Status>();
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<AccountManagerRoleModel, AccountManagerRole>();
                cfg.CreateMap<InventoryTypeModel, InventoryType>();
                cfg.CreateMap<BeneficiaryInventoryModel, BeneficiaryInventory>();
                cfg.CreateMap<AccountManagerInventoryModel, AccountManagerInventory>();

            });
            _mapper = config.CreateMapper();
        }
        public int AddAccountManagerInfo(AccountModel accountModel)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var AccountManager = _mapper.Map<AccountModel, Account>(accountModel);
                dbContext.Accounts.Add(AccountManager); ///?????
                dbContext.SaveChanges();
                return accountModel.Id;
            }
        }

        public void DeleteAccount(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var account = dbContext.Accounts.FirstOrDefault(d => d.Id == id);
                if (account != null)
                {
                    dbContext.Accounts.Remove(account);
                    dbContext.SaveChanges();
                }
            }
        }

        public AccountModel GetAccountById(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var account = dbContext.Accounts.Include(d => d.AccountManagerInfos).Where(account => account.Id == id).FirstOrDefault();
                if (account != null)
                {
                    var accountModel = _mapper.Map<Account, AccountModel>(account);
                    return accountModel;
                }
                return null;
            }
        }

        public IEnumerable<AccountModel> GetAllAccounts()
        {
            using (var dbContext = new NPOManagementContext())
            {
                var accounts = dbContext.Accounts.ToList();

                foreach (var account in accounts)
                {
                    var accountModel = _mapper.Map<Account, AccountModel>(account);
                    yield return accountModel;
                }
            }
        }

        public int ModifyAccount(AccountModel accountModel, int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var account = dbContext.Accounts.FirstOrDefault(d => d.Id == id);
                if (account != null)
                {
                    account.Id =accountModel.Id;
                    account.AccountName =accountModel.AccountName;
                    account.AccountDescription = accountModel.AccountDescription;
                    dbContext.SaveChanges();
                }
                return account.Id;
            }
        }
    }
}
