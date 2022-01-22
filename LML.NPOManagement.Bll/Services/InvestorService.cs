using LML.NPOManagement.Bll.Models;
using LML.NPOManagement.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Services
{
    public class InvestorService
    {
        public IEnumerable<InvestorResponse> GetAllInvestors()
        {
            var investors = new NPOManagementContext().Investors.ToList();
            foreach (var investor in investors)
            {
                yield return new InvestorResponse(investor);
            }
        }

        public InvestorResponse? GetInvestorById(int id)
        {
            var investorEntity = new NPOManagementContext().Investors.Where(investor => investor.Id == id).FirstOrDefault();
            if (investorEntity != null)
            {
                return new InvestorResponse(investorEntity);
            }
            return null;
        }
    }
}
