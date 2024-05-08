using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class UserAccountRole
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Account2User> Account2Users { get; } = new List<Account2User>();
}
