using LML.NPOManagement.Common;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class AddUserToAccountRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required,Range(1,3)]
        public UserAccountRoleEnum UserAccountRoleEnum { get; set; }
    }
}
