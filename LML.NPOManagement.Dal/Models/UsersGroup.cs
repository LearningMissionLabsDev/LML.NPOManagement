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
        public int CreatorId { get; set; }
        public string GroupName { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? Description { get; set; }

        public virtual User Creator { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
