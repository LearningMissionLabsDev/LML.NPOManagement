using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private IMapper _mapper;
        public BeneficiaryService()
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

        public int AddBeneficiary(BeneficiaryModel beneficiaryModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteBeneficiary(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BeneficiaryModel> GetAllBeneficiaries()
        {
            using(var dbContext = new NPOManagementContext())
            {
                var beneficiaries = dbContext.Beneficiaries.ToList();

                foreach (var beneficiary in beneficiaries)
                {
                    var beneficiaryModel = _mapper.Map<Beneficiary, BeneficiaryModel>(beneficiary);
                    yield return beneficiaryModel;
                }
            }
        }

        public BeneficiaryModel GetBeneficiaryById(int id)
        {
            using( var dbContext = new NPOManagementContext())
            {
                var beneficiary = dbContext.Beneficiaries.Where(b => b.Id == id).FirstOrDefault();

                if (beneficiary != null)
                {
                    var beneficiModel = _mapper.Map<Beneficiary, BeneficiaryModel>(beneficiary);
                    return beneficiModel;
                }
                return null;
            }
        }

        public int ModifyBeneficiary(BeneficiaryModel beneficiaryModel, int id)
        {
            throw new NotImplementedException();
        }
    }
}
