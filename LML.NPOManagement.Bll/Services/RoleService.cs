using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Services
{
    public class RoleService : IRoleService
    {
        private IMapper _mapper;
        public RoleService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountManagerInfo, AccountManagerInfoModel>();
                cfg.CreateMap<Beneficiary, BeneficiaryModel>();
                cfg.CreateMap<Role, RoleModel>();
                cfg.CreateMap<Status, StatusModel>();
                cfg.CreateMap<AccountManager, AccountManagerModel>();
                cfg.CreateMap<AccountManagerInfoModel, AccountManagerInfo>();
                cfg.CreateMap<BeneficiaryModel, Beneficiary>();
                cfg.CreateMap<RoleModel, Role>();
                cfg.CreateMap<StatusModel, Status>();
                cfg.CreateMap<AccountManagerModel, AccountManager>();

            });
            _mapper = config.CreateMapper();
        }

        public IEnumerable<RoleModel> GetAllRole()
        {
            throw new NotImplementedException();
        }

        public RoleModel GetRoleById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
