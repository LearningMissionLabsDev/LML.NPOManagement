using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class UserTypeRequest
    {
        [Required]
        [Range (int.MinValue, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
