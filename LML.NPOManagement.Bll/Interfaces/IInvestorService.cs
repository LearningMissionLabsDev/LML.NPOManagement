using LML.NPOManagement.Bll.Model;
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
        public DonationModel ModifyDonation(DonationModel DonationModel, int id);
        public void DeleteDonation(int id);
    }
}
