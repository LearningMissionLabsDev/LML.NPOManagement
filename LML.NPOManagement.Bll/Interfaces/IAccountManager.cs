using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IAccountManager
    {
        public IEnumerable<AccountManagerModel> GetAllAccountManager();
        public AccountManagerModel GetAccountManagerById(int id);
        public int AddAccountManager(AccountManagerModel accountManagerModel);
        public int ModifyAccountManager(AccountManagerModel accountManagerModel, int id);
        public void DeleteAccountManager(int id);
    }
}
