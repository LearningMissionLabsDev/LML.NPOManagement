using LML.NPOManagement.Bll.Model;

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
        Task<List<DonationModel>> GetDonationByYear(DateTime dateTimeStart,DateTime dateTimeFinish);
    }
}
