using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Model
{
    public class UserNotifikationModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public RoleModel UserRole { get; set; }
        public AccountModel UserAccount { get; set; }
    }
}
