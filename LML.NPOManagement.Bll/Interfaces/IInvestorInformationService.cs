using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IInvestorInformationService
    {
        public IEnumerable<InvestorInformationModel> GetAllInvestorInformations();
        public InvestorInformationModel GetInvestorInformationById(int id);
        public int AddInvestorInformation(InvestorInformationModel investorInformationModel);
        public int ModifyInvestorInformation(InvestorInformationModel investorInformationModel, int id);
        public void DeleteInvestorInformation(int id);
    }
}
