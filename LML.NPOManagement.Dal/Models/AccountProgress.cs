

namespace LML.NPOManagement.Dal.Models
{
    public partial class AccountProgress
    {
        /// <summary>
        /// From Progress in account
        /// </summary>
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; } = null!;
        public virtual Account Account { get; set; } = null!;
    }
}
