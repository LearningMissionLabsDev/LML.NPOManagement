using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class UserResponse
    {
        public UserResponse()
        {
            InvestorInformations = new HashSet<InvestorInformationResponse>();
            UserInventories = new HashSet<UserInventoryResponse>();
            Accounts = new HashSet<AccountResponse>();
            Notifications = new HashSet<NotificationResponse>();
        }

        public int Id { get; set; }
        public int UserTypeId { get; set; }
        public int UserInformationId { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string Status { get; set; }

        public virtual UserInformationResponse UserInformation { get; set; } 
        public virtual UserTypeResponse UserType { get; set; } 
        public virtual ICollection<InvestorInformationResponse> InvestorInformations { get; set; }
        public virtual ICollection<UserInventoryResponse> UserInventories { get; set; }

        public virtual ICollection<AccountResponse> Accounts { get; set; }
        public virtual ICollection<NotificationResponse> Notifications { get; set; }
    }
}
