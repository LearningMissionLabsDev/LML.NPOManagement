using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Response
{
    public class AccountUserActivityResponse
    {
        public int Id { get; set; }

        public int Account2UserId { get; set; }

        public DateTime DateCreated { get; set; }

        public string ActivityInfo { get; set; } = null!;
    }
}
