
using System.Security.Principal;

namespace LML.NPOManagement.Common.Model
{
    public class AccountStatusModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public virtual ICollection<AccountModel> Accounts { get; } = new List<AccountModel>();
    }
}
