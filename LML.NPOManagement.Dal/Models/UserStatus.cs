using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class UserStatus
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
