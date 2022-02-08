
namespace LML.NPOManagement.Response
{
    public class DonationResponse
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfCharity { get; set; }
        public virtual InvestorResponse Investor { get; set; }
    }
}
