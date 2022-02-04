using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IAccountManagerInfoService
    {
        public IEnumerable<AccountManagerInfoModel> GetAllAccountManagerInfos();
        public AccountManagerInfoModel GetAccountManagerInfoById(int id);
        public int AddAccountManagerInfo(AccountManagerInfoModel accountManagerInfoModel);
        public int ModifyAccountManagerInfo(AccountManagerInfoModel accountManagerInfoModel, int id);
        public void DeleteAccountManagerInfo(int id);
    }
}
