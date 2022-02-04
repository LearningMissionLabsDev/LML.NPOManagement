using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;

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
                cfg.CreateMap<AccountManager, AccountManagerModel>();
                cfg.CreateMap<AccountManagerRole, AccountManagerRoleModel>();
                cfg.CreateMap<AccountManagerInfoModel, AccountManagerInfo>();
                cfg.CreateMap<BeneficiaryModel, Beneficiary>();
                cfg.CreateMap<BeneficiaryRoleModel, BeneficiaryRole>();
                cfg.CreateMap<StatusModel, Status>();
                cfg.CreateMap<AccountManagerModel, AccountManager>();
                cfg.CreateMap<AccountManagerRoleModel, AccountManagerRole>();

            });
            _mapper = config.CreateMapper();
        }
        public AccountManagerRoleModel GetAccountManagerRoleById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccountManagerRoleModel> GetAllAccountManagerRoles()
        {
            throw new NotImplementedException();
        }
    }
}
