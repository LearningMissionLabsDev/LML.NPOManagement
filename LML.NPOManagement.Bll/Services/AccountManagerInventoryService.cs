using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Services
{
    public class AccountManagerInventoryService : IAccountManagerInventoryService
    {
        private IMapper _mapper;
        public AccountManagerInventoryService()
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
        public int AddAccountManagerInventory(AccountManagerInventoryModel accountManagerInventoryModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccountManagerInventory(int id)
        {
            throw new NotImplementedException();
        }

        public AccountManagerInventoryModel GetAccountManagerInventoryById(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var accountManagerInventories = dbContext.AccountManagerInventories.Where
                (accountManagerInventory => accountManagerInventory.Id == id).FirstOrDefault();

                if (accountManagerInventories != null)
                {
                    var accountManagerInventoriesModel = _mapper.
                    Map<AccountManagerInventory, AccountManagerInventoryModel>(accountManagerInventories);

                    return accountManagerInventoriesModel;
                }
                return null;

            }
        }

        public IEnumerable<AccountManagerInventoryModel> GetAllAccountManagerInventories()
        {
            using (var dbContext = new NPOManagementContext())
            {
                var accountManagerInventories = dbContext.AccountManagerInventories.ToList();

                foreach (var accountManagerInventory in accountManagerInventories)
                {
                    var accountManagerInventoryModel = _mapper.
                    Map<AccountManagerInventory, AccountManagerInventoryModel>(accountManagerInventory);

                    yield return accountManagerInventoryModel;  
                }
            }
        }

        public int ModifyAccountManagerInventory(AccountManagerInventoryModel accountManagerInventoryModel, int id)
        {
            throw new NotImplementedException();
        }
    }
}
