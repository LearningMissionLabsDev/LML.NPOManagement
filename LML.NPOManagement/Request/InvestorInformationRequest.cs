using LML.NPOManagement.Bll.Model;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{ 
    public class InvestorInformationRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
        public InvestorTierEnum? InvestorTierEnum { get; set; }
    }
}
