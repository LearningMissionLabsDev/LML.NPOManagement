

namespace LML.NPOManagement.Bll.Model
{
    public class UserModel
    {
        public UserModel()
        {
            InvestorInformations = new HashSet<InvestorInformationModel>();
            UserInformations = new HashSet<UserInformationModel>();
            UserInventories = new HashSet<UserInventoryModel>();
            Accounts = new HashSet<AccountModel>();
            Notifications = new HashSet<NotificationModel>();
            Roles = new HashSet<RoleModel>();
            UserTypes = new HashSet<UserTypeModel>();
        }

        public int Id { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public string Token { get; set; }
        public StatusEnumModel Status { get; set; }
        public virtual ICollection<InvestorInformationModel> InvestorInformations { get; set; }
        public virtual ICollection<UserInformationModel> UserInformations { get; set; }
        public virtual ICollection<UserInventoryModel> UserInventories { get; set; }
        public virtual ICollection<AccountModel> Accounts { get; set; }
        public virtual ICollection<NotificationModel> Notifications { get; set; }
        public virtual ICollection<RoleModel> Roles { get; set; }
        public virtual ICollection<UserTypeModel> UserTypes { get; set; }
    }
}
