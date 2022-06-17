using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class AccountRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength (int.MaxValue)]
        public string Description { get; set; }

        [StringLength (50)]
        public string Status { get; set; }
    }
}
