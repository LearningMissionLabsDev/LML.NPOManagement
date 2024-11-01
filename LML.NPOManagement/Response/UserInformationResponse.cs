using LML.NPOManagement.Common;

namespace LML.NPOManagement.Response
{
    public class UserInformationResponse
    {
        public RequestedUserTypeEnum UserTypeEnum { get; set; }
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? UserImage { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int AccountRoleId { get; set; }
    }
}