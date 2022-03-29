using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class AccountProgressRequest
    {
        [Required]
        [StringLength (250)]
        public string Description { get; set; }
       
    }
}
