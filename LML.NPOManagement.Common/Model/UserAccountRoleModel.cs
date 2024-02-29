using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Common.Model;

public class UserAccountRoleModel
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public virtual ICollection<Account2UserModel> Account2Users { get; } = new List<Account2UserModel>();
}
