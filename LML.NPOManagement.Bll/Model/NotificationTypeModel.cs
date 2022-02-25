using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Bll.Model
{ 
    public class NotificationTypeModel
    {
        public NotificationTypeModel()
        {
            Notifications = new HashSet<NotificationModel>();
        }

        public int Id { get; set; }
        public string Description { get; set; } 

        public virtual ICollection<NotificationModel> Notifications { get; set; }
    }
}
