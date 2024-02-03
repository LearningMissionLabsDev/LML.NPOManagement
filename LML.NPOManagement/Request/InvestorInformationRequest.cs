using LML.NPOManagement.Common;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{ 
    public class InvestorInformationRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Range(1,3)]
        public InvestorTierEnum InvestorTierEnum { get; set; }
    }
}
