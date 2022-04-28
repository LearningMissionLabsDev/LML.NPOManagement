using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class LoginRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,25}$",
        ErrorMessage = "Password must be between 6 and 25 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string Password { get; set; }
    }
}
