using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class InvestorTierTypeRequest
    {
        [Required]
        [StringLength(50)]
        public string InvestorTier { get; set; }
    }
}
