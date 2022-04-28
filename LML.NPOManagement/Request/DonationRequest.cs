using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class DonationRequest 
    {
        [Required]
        [Range(1,int.MaxValue)]
        public decimal Amount { get; set; }

        [Range(1,3)]
        public int InvestorId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfCharity { get; set; }
    }
}
