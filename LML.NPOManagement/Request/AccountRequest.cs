using LML.NPOManagement.Common;
using LML.NPOManagement.Dal.Models;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class AccountRequest
    {
        public bool? IsVisible { get; set; }
        public int? MaxCapacity { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? OnboardingLink { get; set; }
        public string? Description { get; set; }
        public int StatusId { get; set; }
        //public AccountStatusEnum StatusEnum { get; set; }
    }
}
