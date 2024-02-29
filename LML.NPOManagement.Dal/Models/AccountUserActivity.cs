using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class AccountUserActivity
{
    public int Id { get; set; }

    public int Account2UserId { get; set; }

    public DateTime DateCreated { get; set; }

    public string ActivityInfo { get; set; } = null!;

    public virtual Account2User Account2User { get; set; } = null!;
}
