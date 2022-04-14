
namespace LML.NPOManagement.Bll.Model
{
    public class DonationModel
    {
        public int Id { get; set; }
        public int InvestorId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfCharity { get; set; }
        public virtual InvestorInformationModel Investor { get; set; } 
    }
}
