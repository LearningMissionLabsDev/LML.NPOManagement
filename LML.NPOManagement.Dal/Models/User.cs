namespace LML.NPOManagement.Dal.Models;

public partial class User
{
    public int Id { get; set; }

    public int StatusId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Account2User> Account2Users { get; } = new List<Account2User>();

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();

    public virtual ICollection<InvestorInformation> InvestorInformations { get; } = new List<InvestorInformation>();

    public virtual UserStatus Status { get; set; } = null!;

    public virtual ICollection<UserIdea> UserIdeas { get; } = new List<UserIdea>();

    public virtual ICollection<UserInformation> UserInformations { get; } = new List<UserInformation>();

    public virtual ICollection<UserInventory> UserInventories { get; } = new List<UserInventory>();

    public virtual ICollection<UsersGroup> UsersGroups { get; } = new List<UsersGroup>();

    public virtual ICollection<UsersGroup> Groups { get; } = new List<UsersGroup>();

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();
}
