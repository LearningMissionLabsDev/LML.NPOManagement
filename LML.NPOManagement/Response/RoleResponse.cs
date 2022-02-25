using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Response
{
    public class RoleResponse
    {
        public RoleResponse()
        {
            Users = new HashSet<UserResponse>();
        }

        public int Id { get; set; }
        public string UserRole { get; set; } = null!;

        public virtual ICollection<UserResponse> Users { get; set; }
    }
}
