using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string UserRole { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
