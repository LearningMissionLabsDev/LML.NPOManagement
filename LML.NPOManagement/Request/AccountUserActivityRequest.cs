using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Request
{
    public class AccountUserActivityRequest
    {
        public int Account2UserId { get; set; }

        public DateTime DateCreated { get; set; }

        public string ActivityInfo { get; set; } = null!;
    }
}
