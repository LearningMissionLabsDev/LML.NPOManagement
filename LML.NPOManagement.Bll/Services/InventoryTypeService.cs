using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Services
{
    public class InventoryTypeService : IInventoryTypeService
    {
        private IMapper _mapper;
        public InventoryTypeService()
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
                cfg.CreateMap<BeneficiaryInventory,BeneficiaryInventoryModel>();
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
        public int AddInventoryType(InventoryTypeModel inventoryTypeModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteInventoryType(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InventoryTypeModel> GetAllInventoryTypes()
        {
            using(var dbContext = new NPOManagementContext())
            {
                var inventoryTypes = dbContext.InventoryTypes.ToList();

                foreach (var inventoryType in inventoryTypes)
                {
                    var inventoryTypeModel = _mapper.Map<InventoryType, InventoryTypeModel>(inventoryType);
                    yield return inventoryTypeModel;
                }
            }
        }

        public InventoryTypeModel GetInventoryTypeById(int id)
        {
            using( var dbContext = new NPOManagementContext())
            {
                var inventoryType = dbContext.InventoryTypes.Where(it => it.Id == id).FirstOrDefault();

                if (inventoryType != null)
                {
                    var inventoryTypeModel = _mapper.Map<InventoryType, InventoryTypeModel>(inventoryType);

                    return inventoryTypeModel;
                }
                return null;
            }
        }

        public int ModifyInventoryType(InventoryTypeModel inventoryTypeModel, int id)
        {
            throw new NotImplementedException();
        }
    }
}
