
namespace LML.NPOManagement.Dal.Models
{
    public partial class AccountManagerRole
    {
        public AccountManagerRole()
        {
            AccountManagerInfos = new HashSet<AccountManagerInfo>();
        }

        public int Id { get; set; }
        public string AccountManagerRoleType { get; set; } = null!;

        public virtual ICollection<AccountManagerInfo> AccountManagerInfos { get; set; }
    }
}
