using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IAccountManagerInfoService
    {
        public IEnumerable<AccountManagerInfoModel> GetAllAccountManagerInfo();
        public AccountManagerInfoModel GetAccountManagerInfoById(int id);
        public int AddAccountManagerInfo(AccountManagerInfoModel accountManagerInfoModel);
        public int ModifyAccountManagerInfo(AccountManagerInfoModel accountManagerInfoModel, int id);
        public void DeleteAccountManagerInfo(int id);
    }
}
