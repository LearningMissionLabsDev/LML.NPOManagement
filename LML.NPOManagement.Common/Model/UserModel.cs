﻿using LML.NPOManagement.Common.Model;

namespace LML.NPOManagement.Common
{
    public class UserModel
    {
        public int Id { get; set; }

        public int? StatusId { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Token { get; set; }

        public bool IsSystemAdmin { get; set; }

        public ICollection<Account2UserModel> Account2Users { get; set; } = new List<Account2UserModel>();

        public virtual ICollection<AccountModel> Accounts { get; } = new List<AccountModel>();

        public virtual ICollection<InvestorInformationModel> InvestorInformations { get; } = new List<InvestorInformationModel>();

        public virtual UserStatusModel Status { get; set; } = null!;

        public virtual ICollection<UserIdeaModel> UserIdeas { get; } = new List<UserIdeaModel>();

        public virtual ICollection<UserInformationModel> UserInformations { get; } = new List<UserInformationModel>();

        public virtual ICollection<UserInventoryModel> UserInventories { get; } = new List<UserInventoryModel>();

        public virtual ICollection<UsersGroupModel> UsersGroups { get; } = new List<UsersGroupModel>();

        public virtual ICollection<UsersGroupModel> Groups { get; } = new List<UsersGroupModel>();

        public virtual ICollection<NotificationModel> Notifications { get; } = new List<NotificationModel>();
    }
}
