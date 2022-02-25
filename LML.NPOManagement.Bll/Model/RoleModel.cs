using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Model
{
    public class RoleModel
    {
        public RoleModel()
        {
            Users = new HashSet<UserModel>();
        }

        public int Id { get; set; }
        public string UserRole { get; set; } = null!;

        public virtual ICollection<UserModel> Users { get; set; }
    }
}
