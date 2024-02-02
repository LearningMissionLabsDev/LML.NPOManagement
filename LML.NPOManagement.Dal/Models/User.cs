using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class User
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int? StatusId { get; set; }
    public virtual ICollection<InvestorInformation> InvestorInformations { get; set; } = new List<InvestorInformation>();
    public virtual UserStatus? Status { get; set; }
    public virtual ICollection<UserIdea> UserIdeas { get; set; } = new List<UserIdea>();
    public virtual ICollection<UserInformation> UserInformations { get; set; } = new List<UserInformation>();
    public virtual ICollection<UserInventory> UserInventories { get; set; } = new List<UserInventory>();
    public virtual ICollection<UsersGroup> UsersGroups { get; set; } = new List<UsersGroup>();
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    public virtual ICollection<UsersGroup> Groups { get; set; } = new List<UsersGroup>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public virtual ICollection<UserType> UserTypes { get; set; } = new List<UserType>();
}
