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

        public IEnumerable<AccountManagerInfoModel> GetAllAccountManagerInfo()
        {
            throw new NotImplementedException();
        }

        public int ModifyAccountManagerInfo(AccountManagerInfoModel accountManagerInfoModel, int id)
        {
            throw new NotImplementedException();
        }
    }
}
