using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class InventoryTypeRequest
    {
        [Required]
        [StringLength(100)]        
        public string Description { get; set; }
    }
}
