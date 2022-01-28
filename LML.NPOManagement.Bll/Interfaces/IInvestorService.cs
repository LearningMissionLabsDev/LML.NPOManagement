using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Independencies
{
    public interface IInvestorService
    {
        public IEnumerable<InvestorModel> GetAllInvestors();
        public InvestorModel GetInvestorById(int id);
    }
}
