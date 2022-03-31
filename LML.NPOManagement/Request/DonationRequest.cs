using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class DonationRequest
    {
        [Required]
        [Range ((double)Decimal.MinValue,(double) Decimal.MaxValue)]
        public decimal Amount { get; set; }
        
        [Required]
        [DataType (DataType.DateTime)]
        public DateTime DateOfCharity { get; set; }
    }
}
