using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Services
{
    public class StatusService : IStatusService
    {
        private IMapper _mapper;
        public StatusService()
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

        public IEnumerable<StatusModel> GetAllStatus()
        {
            using (var dbContext = new NPOManagementContext())
            {
                var statuses = dbContext.Statuses.ToList();

                foreach (var status in statuses)
                {
                    var statusModel = _mapper.Map<Status,StatusModel>(status);
                    yield return statusModel;
                }
            }
            
        }

        public StatusModel GetStatusById(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var status = dbContext.statuses.Where(status => status.Id == id).FirstOrDefault();
                if (status != null)
                {
                    var statusModel = _mapper.Map<Status,StatusModel>(status);
                    return statusModel;
                }
                return null;
            }
           
        }
    }
}
