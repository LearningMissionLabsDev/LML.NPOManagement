
namespace LML.NPOManagement.Bll.Model
{
    public class AccountProgressModel
    {
        /// <summary>
        /// From Progress in account
        /// </summary>
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; } 
        public virtual AccountModel Account { get; set; } 
    }
}
