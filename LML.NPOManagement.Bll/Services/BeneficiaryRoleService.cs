using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Bll.Services
{
    public class BeneficiaryRoleService : IBeneficiaryRoleService
    {
        private IMapper _mapper;
        public BeneficiaryRoleService()
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

        public IEnumerable<BeneficiaryRoleModel> GetAllBeneficiaryRoles()
        {
            using (var dbContext = new NPOManagementContext())
            {
                var beneficiaryRoles = dbContext.BeneficiaryRoles.ToList();

                foreach (var beneficiaryRole in beneficiaryRoles)
                {
                    var beneficiaryRoleModel = _mapper.Map<BeneficiaryRole, BeneficiaryRoleModel>(beneficiaryRole);
                    yield return beneficiaryRoleModel;
                }
            }
        }

        public BeneficiaryRoleModel GetBeneficiaryRoleById(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var beneficiaryRole = dbContext.BeneficiaryRoles.Include(d => d.Beneficiaries).Where(beneficiaryRole => beneficiaryRole.Id == id).FirstOrDefault();
                if (beneficiaryRole != null)
                {
                    var beneficiaryRoleModel = _mapper.Map<BeneficiaryRole, BeneficiaryRoleModel>(beneficiaryRole);
                    return beneficiaryRoleModel;
                }
                return null;
            }
        }

    }
}
