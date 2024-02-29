using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class Account2User
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int UserId { get; set; }

    public int AccountRoleId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual UserAccountRole AccountRole { get; set; } = null!;

    public virtual ICollection<AccountUserActivity> AccountUserActivities { get; } = new List<AccountUserActivity>();

    public virtual User User { get; set; } = null!;
}
