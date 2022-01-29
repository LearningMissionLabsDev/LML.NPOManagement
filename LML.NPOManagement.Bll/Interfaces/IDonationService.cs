using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Independencies
{
    public interface IDonationService
    {
        public IEnumerable<DonationModel> GetAllDonations();
        public DonationModel GetDonationById(int id);
        public int AddDonation(DonationModel donationModel);
        public int ModifyDonation(DonationModel donationModel, int id);    
        public void DeleteDonation(int id);
    }
}
