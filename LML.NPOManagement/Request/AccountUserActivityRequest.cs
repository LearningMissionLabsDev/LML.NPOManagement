using LML.NPOManagement.Dal.Models;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class AccountUserActivityRequest
    {
        [Required]
        public int Account2UserId { get; set; }
        public string ActivityInfo { get; set; } = null!;
    }
}
