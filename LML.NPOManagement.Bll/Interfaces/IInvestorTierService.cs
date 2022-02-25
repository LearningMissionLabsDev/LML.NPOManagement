using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IInvestorTierService
    {
        public IEnumerable<InvestorTierModel> GetAllInvestorTiers();
        public InvestorTierModel GetInvestorTierById(int id);
        public int AddInvestorTier(InvestorTierModel investorTierModel);
        public int ModifyInvestorTier(InvestorTierModel investorTierModel, int id);
        public void DeleteInvestorTier(int id);
    }
}
