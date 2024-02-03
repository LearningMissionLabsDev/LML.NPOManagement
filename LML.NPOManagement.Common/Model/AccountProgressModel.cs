using LML.NPOManagement.Common.Model;

namespace LML.NPOManagement.Common
{
    public class AccountProgressModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; } = null!;
        public virtual AccountModel Account { get; set; } = null!;
    }
}
