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
        Task<List<InvestorInformationModel>> GetAllInvestorInformations();
        Task<List<DonationModel>> GetAllDonation();
        Task<InvestorInformationModel> GetInvestorInformationById(int id);
        Task<DonationModel> GetDonationById(int id);
        Task<int> AddDonation(DonationModel donationModel);
        Task<DonationModel> ModifyDonation(DonationModel DonationModel, int id);
        public void DeleteDonation(int id);
    }
}
