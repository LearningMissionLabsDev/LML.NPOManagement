using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class NotificationTranportTypeRequest
    {
        [Required]
        [StringLength(100)]
        public string Description { get; set; } 
    }
}
