using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class InvestorRequest
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
       
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public int PhoneNumber { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
    }
}
