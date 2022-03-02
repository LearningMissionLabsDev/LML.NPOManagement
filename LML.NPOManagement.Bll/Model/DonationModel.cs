using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class DonationModel
    {
        public DonationModel(Donation donation)
        {
            donation.Id = Id;
            donation.InvestorId = InvestorId;
            donation.Amount = Amount;
            donation.DateOfCharity = DateOfCharity;
        }
        public int Id { get; set; }
        public int InvestorId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfCharity { get; set; }

        public virtual InvestorInformationModel Investor { get; set; } 
    }
}
