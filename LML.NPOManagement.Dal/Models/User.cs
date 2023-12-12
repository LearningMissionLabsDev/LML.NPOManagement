using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class User
    {
        public User()
        {
            InvestorInformations = new HashSet<InvestorInformation>();
            UserIdeas = new HashSet<UserIdea>();
            UserInformations = new HashSet<UserInformation>();
            UserInventories = new HashSet<UserInventory>();
            UsersGroups = new HashSet<UsersGroup>();
            Accounts = new HashSet<Account>();
            Groups = new HashSet<UsersGroup>();
            Notifications = new HashSet<Notification>();
            Roles = new HashSet<Role>();
            UserTypes = new HashSet<UserType>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Status { get; set; }

        public virtual ICollection<InvestorInformation> InvestorInformations { get; set; }
        public virtual ICollection<UserIdea> UserIdeas { get; set; }
        public virtual ICollection<UserInformation> UserInformations { get; set; }
        public virtual ICollection<UserInventory> UserInventories { get; set; }
        public virtual ICollection<UsersGroup> UsersGroups { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<UsersGroup> Groups { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserType> UserTypes { get; set; }
    }
}
