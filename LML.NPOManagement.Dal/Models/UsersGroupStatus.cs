using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class UsersGroupStatus
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<UsersGroup> UsersGroups { get; } = new List<UsersGroup>();
}
