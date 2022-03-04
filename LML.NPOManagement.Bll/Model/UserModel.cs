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
        }
        public int Id { get; set; }
        public int UserTypeId { get; set; }
        public int UserInformationId { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string Status { get; set; }

        public virtual UserInformationModel UserInformation { get; set; } 
        public virtual UserTypeModel UserType { get; set; } 
        public virtual ICollection<InvestorInformationModel> InvestorInformations { get; set; }
        public virtual ICollection<UserInventoryModel> UserInventories { get; set; }

        public virtual ICollection<AccountModel> Accounts { get; set; }
        public virtual ICollection<NotificationModel> Notifications { get; set; }
    }
}
