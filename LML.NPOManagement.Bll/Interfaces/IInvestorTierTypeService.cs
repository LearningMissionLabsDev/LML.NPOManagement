using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IInvestorTierTypeService
    {
        public IEnumerable<InvestorTierTypeModel> GetAllInvestorTierTypes();
        public InvestorTierTypeModel GetInvestorTierTypeById(int id);
        public int AddInvestorTierType(InvestorTierTypeModel investorTierTypeModel);
        public int ModifyInvestorTierType(InvestorTierTypeModel investorTierTypeModel, int id);
        public void DeleteInvestorTierType(int id);
    }
}
