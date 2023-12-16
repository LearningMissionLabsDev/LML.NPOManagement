using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class UsersGroup
    {
        public UsersGroup()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public int CreatedByUserId { get; set; }
        public string GroupName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string? Description { get; set; }

        public virtual User CreatedByUser { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
