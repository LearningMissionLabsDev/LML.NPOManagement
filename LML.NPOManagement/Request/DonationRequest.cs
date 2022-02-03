using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class DonationRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "")]
        public int InvestorId { get; set; }
        [Required]
        [Range(10, double.MaxValue)]
        public decimal Amount { get; set; }
        [Required]
        public DateTime DateOfCharity { get; set; }
    }
}
