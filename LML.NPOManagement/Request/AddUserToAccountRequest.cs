using LML.NPOManagement.Common;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class AddUserToAccountRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int AccountId { get; set; }
        [Required]
        public UserAccountRoleEnum UserAccountRoleEnum { get; set; }
    }
}