
namespace LML.NPOManagement.Bll.Model
{
    public class AccountManagerRoleModel
    {
        public AccountManagerRoleModel()
        {
            AccountManagerInfos = new HashSet<AccountManagerInfoModel>();
        }

        public int Id { get; set; }
        public string AccountManagerRoleType { get; set; } = null!;

        public virtual ICollection<AccountManagerInfoModel> AccountManagerInfos { get; set; }
    }
}
