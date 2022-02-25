using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IAccountProgressService
    {
        public IEnumerable<AccountProgressModel> GetAllAccountProgressis();
        public AccountProgressModel GetAccountProgressById(int id);
        public int AddAccountProgress(AccountProgressModel accountProgressModel);
        public int ModifyAccountProgress(AccountProgressModel accountProgressModel, int id);
        public void DeleteAccountProgress(int id);
    }
}
