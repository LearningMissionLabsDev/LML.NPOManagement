using LML.NPOManagement.Common;

namespace LML.NPOManagement.Response
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public int? StatusId { get; set; }
        public int CreatorId { get; set; }
        public bool? IsVisible { get; set; }
        public AccountStatusEnum StatusEnum { get; set; }
        public string Name { get; set; } = null!;
        public string? OnboardingLink { get; set; }
        public int? MaxCapacity { get; set; }
        public string? AccountImage { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? AccountRoleId { get; set; }
    }
}
