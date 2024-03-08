using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Common.Model;

public class Account2UserModel
{
    public int AccountId { get; set; }
    public int UserId { get; set; }
    public int AccountRoleId { get; set; }
    public string Token { get; set; }
    public UserAccountRoleEnum Role { get; set; }
    public virtual AccountModel Account { get; set; } = null!;
    public virtual UserAccountRoleModel AccountRole { get; set; } = null!;
    public virtual UserModel User { get; set; } = null!;
}
