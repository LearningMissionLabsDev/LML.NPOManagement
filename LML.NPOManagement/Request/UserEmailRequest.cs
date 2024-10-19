using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class UserEmailRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

    }
}
