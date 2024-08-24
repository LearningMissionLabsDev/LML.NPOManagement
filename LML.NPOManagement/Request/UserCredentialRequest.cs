using LML.NPOManagement.Common;

namespace LML.NPOManagement.Request
{
    public class UserCredentialRequest
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public int RequestedUserRoleId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public Gender Gender { get; set; }
        public string? UserImage { get; set; }
        public string? Metadata { get; set; }
    }
}
