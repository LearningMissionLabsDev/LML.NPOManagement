using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Bll.Services
{
    public class AccountManagerRoleService : IAccountManagerRoleService
    {
        private IMapper _mapper;
        public AccountManagerRoleService()
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
        public AccountManagerRoleModel GetAccountManagerRoleById(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var accountManagerRole = dbContext.AccountManagerRoles.Include(d => d.AccountManagerInfos).Where(accountManagerRole => accountManagerRole.Id == id).FirstOrDefault();
                if (accountManagerRole != null)
                {
                    var accountManagerRoleModel = _mapper.Map<AccountManagerRole, AccountManagerRoleModel>(accountManagerRole);
                    return accountManagerRoleModel;
                }
                return null;
            }
        }

        public IEnumerable<AccountManagerRoleModel> GetAllAccountManagerRoles()
        {
            using (var dbContext = new NPOManagementContext())
            {
                var accountManagerRoles = dbContext.AccountManagerRoles.ToList();

                foreach (var accountManagerRole in accountManagerRoles)
                {
                    var accountManagerRoleModel = _mapper.Map<AccountManagerRole, AccountManagerRoleModel>(accountManagerRole);
                    yield return accountManagerRoleModel;
                }
            }
        }
    }
}
