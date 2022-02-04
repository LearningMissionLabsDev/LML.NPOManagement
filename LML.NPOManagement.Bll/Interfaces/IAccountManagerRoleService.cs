using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IAccountManagerRoleService
    {
        public IEnumerable<AccountManagerRoleModel> GetAllAccountManagerRoles();
        public AccountManagerRoleModel GetAccountManagerRoleById(int id);

    }
}
