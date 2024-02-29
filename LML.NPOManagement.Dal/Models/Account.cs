using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class Account
{
    public int Id { get; set; }

    public int StatusId { get; set; }

    public int CreatorId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Account2User> Account2Users { get; } = new List<Account2User>();

    public virtual User Creator { get; set; } = null!;

    public virtual AccountStatus Status { get; set; } = null!;
}
