using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class User
    {
        public User()
        {
            InvestorInformations = new HashSet<InvestorInformation>();
            UserInventories = new HashSet<UserInventory>();
            Accounts = new HashSet<Account>();
            Notifications = new HashSet<Notification>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public int UserTypeId { get; set; }
        public int UserInformationId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Status { get; set; }

        public virtual UserInformation UserInformation { get; set; } = null!;
        public virtual UserType UserType { get; set; } = null!;
        public virtual ICollection<InvestorInformation> InvestorInformations { get; set; }
        public virtual ICollection<UserInventory> UserInventories { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
