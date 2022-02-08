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
                cfg.CreateMap<AccountManagerInfoModel, AccountManagerInfo>();
                cfg.CreateMap<BeneficiaryModel, Beneficiary>();
                cfg.CreateMap<BeneficiaryRoleModel, BeneficiaryRole>();
                cfg.CreateMap<StatusModel, Status>();
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<AccountManagerRoleModel, AccountManagerRole>();

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
            throw new NotImplementedException();
        }

        public BeneficiaryModel GetBeneficiaryById(int id)
        {
            throw new NotImplementedException();
        }

        public int ModifyBeneficiary(BeneficiaryModel beneficiaryModel, int id)
        {
            throw new NotImplementedException();
        }
    }
}
