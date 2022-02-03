using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Services
{
    public class AccountManagerServices : IAccountManager
    {
        private IMapper _mapper;
        public AccountManagerServices()
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

        public int AddAccountManager(AccountManagerModel accountManagerModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccountManager(int id)
        {
            throw new NotImplementedException();
        }

        public AccountManagerModel GetAccountManagerById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccountManagerModel> GetAllAccountManager()
        {
            throw new NotImplementedException();
        }

        public int ModifyAccountManager(AccountManagerModel accountManagerModel, int id)
        {
            throw new NotImplementedException();
        }
    }
}
