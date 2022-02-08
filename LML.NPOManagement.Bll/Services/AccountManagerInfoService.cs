using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Services
{
    public class AccountManagerInfoService : IAccountManagerInfoService
    {
        private IMapper _mapper;
        public AccountManagerInfoService()
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

        public int AddAccountManagerInfo(AccountManagerInfoModel accountManagerInfoModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccountManagerInfo(int id)
        {
            throw new NotImplementedException();
        }

        public AccountManagerInfoModel GetAccountManagerInfoById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccountManagerInfoModel> GetAllAccountManagerInfos()
        {
            throw new NotImplementedException();
        }

        public int ModifyAccountManagerInfo(AccountManagerInfoModel accountManagerInfoModel, int id)
        {
            throw new NotImplementedException();
        }
    }
}
