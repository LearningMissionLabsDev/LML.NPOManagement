using LML.NPOManagement.Common;

namespace LML.NPOManagement.Request
{
    public class AddUserToAccountRequest
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public UserAccountRoleEnum UserAccountRoleEnum { get; set; }
    }
}
