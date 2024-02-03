using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class Account
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int? StatusId { get; set; }
    public virtual ICollection<AccountProgress> AccountProgresses { get; } = new List<AccountProgress>();
    public virtual AccountStatus? Status { get; set; }
    public virtual ICollection<User> Users { get; } = new List<User>();
}
