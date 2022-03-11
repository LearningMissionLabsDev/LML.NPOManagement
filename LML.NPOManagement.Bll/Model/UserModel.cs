using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class UserModel
    {
        public UserModel()
        {
            InvestorInformations = new HashSet<InvestorInformationModel>();
            UserInventories = new HashSet<UserInventoryModel>();
            Accounts = new HashSet<AccountModel>();
            Notifications = new HashSet<NotificationModel>();
            Roles = new HashSet<RoleModel>();
            UserTypes = new HashSet<UserTypeModel>();
        }

        public int Id { get; set; }
        public int UserInformationId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Token { get; set; } = null!;
        public StatusEnumModel Status { get; set; }

        public virtual UserInformationModel UserInformation { get; set; } = null!;
        public virtual ICollection<InvestorInformationModel> InvestorInformations { get; set; }
        public virtual ICollection<UserInventoryModel> UserInventories { get; set; }

        public virtual ICollection<AccountModel> Accounts { get; set; }
        public virtual ICollection<NotificationModel> Notifications { get; set; }
        public virtual ICollection<RoleModel> Roles { get; set; }
        public virtual ICollection<UserTypeModel> UserTypes { get; set; }
    }
}
