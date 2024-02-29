using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class AccountStatus
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
