using LML.NPOManagement.Common;
using LML.NPOManagement.Dal.Models;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class AccountRequest
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Range(1,3)]
        public AccountStatusEnum StatusEnum { get; set; }  
    }
}
