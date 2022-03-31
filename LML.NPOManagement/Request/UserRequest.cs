using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class UserRequest : LoginRequest
    {
        [Required]
        [StringLength(25)]
        [Range(6, 25)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$",
           ErrorMessage = "Password must be between 6 and 25 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string ConfirmPassword { get; set; }
    }
}
