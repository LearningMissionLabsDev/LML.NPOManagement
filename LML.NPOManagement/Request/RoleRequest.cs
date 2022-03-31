using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{    
    public class RoleRequest
    {
        [Required]
        [StringLength(50)]
        public string UserRole { get; set; } 
    }
}
