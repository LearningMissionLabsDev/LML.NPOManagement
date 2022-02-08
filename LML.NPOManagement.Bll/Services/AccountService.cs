using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Services
{
    public class AccountService : IAccountService
    {
        private IMapper _mapper;
        public AccountService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountManagerInfo, AccountManagerInfoModel>();
                cfg.CreateMap<Beneficiary, BeneficiaryModel>();
                cfg.CreateMap<BeneficiaryRole, BeneficiaryRoleModel>();
                cfg.CreateMap<Status, StatusModel>();
                cfg.CreateMap<Account, AccountModel>();
                cfg.CreateMap<AccountManagerRole, AccountManagerRoleModel>();
                cfg.CreateMap<AccountManagerInfoModel, AccountManagerInfo>();
                cfg.CreateMap<BeneficiaryModel, Beneficiary>();
                cfg.CreateMap<BeneficiaryRoleModel, BeneficiaryRole>();
                cfg.CreateMap<StatusModel, Status>();
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<AccountManagerRoleModel, AccountManagerRole>();

            });
            _mapper = config.CreateMapper();
        }
        public int AddAccountManagerInfo(AccountModel accountModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccount(int id)
        {
            throw new NotImplementedException();
        }

        public AccountModel GetAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccountModel> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public int ModifyAccount(AccountModel accountModel, int id)
        {
            throw new NotImplementedException();
        }
    }
}
