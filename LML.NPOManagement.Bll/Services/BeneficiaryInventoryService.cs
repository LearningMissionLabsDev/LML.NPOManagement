using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Services
{
    public class BeneficiaryInventoryService : IBeneficiaryInventoryService
    {
        private IMapper _mapper;
        public BeneficiaryInventoryService()
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
        public int AddBeneficiaryInventory(BeneficiaryInventoryModel beneficiaryInventoryModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteBeneficiaryInventory(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BeneficiaryInventoryModel> GetAllBeneficiaryInventories()
        {
            using(var dbContext = new NPOManagementContext())
            {
                var beneficiaryInventories = dbContext.BeneficiaryInventories.ToList();

                foreach (var beneficiaryInventory in beneficiaryInventories)
                {
                    var beneficiaryInventoryModel = _mapper.
                    Map<BeneficiaryInventory, BeneficiaryInventoryModel>(beneficiaryInventory);

                    yield return beneficiaryInventoryModel;
                }
            }
        }

        public BeneficiaryInventoryModel GetBeneficiaryInventoryById(int id)
        {
            using( var dbContext = new NPOManagementContext())
            {
                var beneficiaryInventory = dbContext.BeneficiaryInventories.
                Where(bi => bi.Id == id).FirstOrDefault();

                if(beneficiaryInventory != null)
                {
                    var beneficiaryInventoryModel = _mapper.
                    Map<BeneficiaryInventory, BeneficiaryInventoryModel>(beneficiaryInventory);

                    return beneficiaryInventoryModel;
                }
                return null;
            }
        }

        public int ModifyBeneficiaryInventory(BeneficiaryInventoryModel beneficiaryInventoryModel, int id)
        {
            throw new NotImplementedException();
        }
    }
}
