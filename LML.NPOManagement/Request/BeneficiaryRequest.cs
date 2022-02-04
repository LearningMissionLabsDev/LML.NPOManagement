namespace LML.NPOManagement.Request
{
    public class BeneficiaryRequest
    {
        public int BeneficiaryRoleId { get; set; }
        public int StatusId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Gender { get; set; } = null!;

    }
}
