using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class AccountRequest
    {
        [Required]
        public string Name { get; set; } = null!;
        public bool? IsVisible { get; set; }
        public int? MaxCapacity { get; set; }
        public string? OnboardingLink { get; set; }
        public string? Description { get; set; }
        public int StatusId { get; set; }
        public string? AccountImage { get; set; }
    }
}
