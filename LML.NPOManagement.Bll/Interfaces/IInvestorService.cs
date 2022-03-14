using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IInvestorService
    {
        public IEnumerable<InvestorInformationModel> GetAllInvestorInformations();
        public IEnumerable<DonationModel> GetAllDonation();
        public InvestorInformationModel GetInvestorInformationById(int id);
        public DonationModel GetDonationById(int id);
        public int AddInvestorInformation(InvestorInformationModel investorInformationModel);
        public InvestorInformationModel ModifyInvestorInformation(InvestorInformationModel investorInformationModel, int id);
        public void DeleteInvestorInformation(int id);
        public void AddDonationById(int Id);
        public IEnumerable<DonationModel> GetYearlyDonation(DateTime year);
    }
}
