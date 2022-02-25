using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IRoleService
    {
        public IEnumerable<RoleModel> GetAllRoles();
        public RoleModel GetRoleById(int id);
        public int AddRole(RoleModel roleModel);
        public int ModifyRole(RoleModel roleModel, int id);
        public void DeleteRole(int id);
    }
}
