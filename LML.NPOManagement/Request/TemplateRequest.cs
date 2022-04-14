using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class TemplateRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int TemplateTypeId { get; set; }
        public byte[] Uri { get; set; } 
    }
}
