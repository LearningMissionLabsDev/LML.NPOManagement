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

        public int AddAccountManagerInfo(AccountManagerInfoModel accountManagerInfoModel)
        {
            Console.WriteLine(accountManagerInfoModel);

            using (var dbContext = new NPOManagementContext())
            {
                var accountManager = _mapper.Map<AccountManagerInfoModel, AccountManagerInfo>(accountManagerInfoModel);
                dbContext.AccountManagerInfos.Add(accountManager);
                dbContext.SaveChanges();

                return accountManager.Id;
            }
        }

        public void DeleteAccountManagerInfo(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var accountManagerInfo = dbContext.AccountManagerInfos.FirstOrDefault(ami => ami.Id == id);
                
                if(accountManagerInfo != null)
                {
                    dbContext.AccountManagerInfos.Remove(accountManagerInfo);
                    dbContext.SaveChanges();
                }
            }
        }

        public AccountManagerInfoModel GetAccountManagerInfoById(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var accountManagerInfo = dbContext.AccountManagerInfos.Where(ami => ami.Id == id).FirstOrDefault();

                if (accountManagerInfo != null)
                {
                    var accountManagerInfoModel = _mapper.Map<AccountManagerInfo, AccountManagerInfoModel>(accountManagerInfo);
                    return accountManagerInfoModel;
                }

                return null;
            }
        }

        public IEnumerable<AccountManagerInfoModel> GetAllAccountManagerInfos()
        {
            using (var dbContext = new NPOManagementContext())
            {
                var accountManagerInfos = dbContext.AccountManagerInfos.ToList();

                foreach (var accountManagerInfo in accountManagerInfos)
                {
                    var accountManagerInfoModel = _mapper.Map<AccountManagerInfo, AccountManagerInfoModel>(accountManagerInfo);
                    yield return accountManagerInfoModel;
                }
            }
        }

        public int ModifyAccountManagerInfo(AccountManagerInfoModel accountManagerInfoModel, int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var accountManagerInfo = dbContext.AccountManagerInfos.FirstOrDefault(ami => ami.Id == id);
                if (accountManagerInfo != null)
                {
                    accountManagerInfo.Id = id;
                    accountManagerInfo.AccountManagerInfoRoleId = accountManagerInfoModel.AccountManagerInfoRoleId;
                    accountManagerInfo.FirstName = accountManagerInfoModel.FirstName;
                    accountManagerInfo.LastName = accountManagerInfoModel.LastName;
                    accountManagerInfo.MiddleName = accountManagerInfoModel.MiddleName;
                    accountManagerInfo.DateOfBirth = accountManagerInfoModel.DateOfBirth;
                    accountManagerInfo.CreateDate = accountManagerInfoModel.CreateDate;
                    accountManagerInfo.UpdateDate = accountManagerInfoModel.UpdateDate;
                    accountManagerInfo.Email = accountManagerInfoModel.Email;
                    accountManagerInfo.PhoneNumber = accountManagerInfoModel.PhoneNumber;
                    accountManagerInfo.Information = accountManagerInfoModel.Information;
                    //accountManagerInfo.Gender = accountManagerInfoModel.Gender;
                }

                return accountManagerInfo.Id;
            }
        }
    }
}
