using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Model
{
    public class NotificationArchiveModel
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public DateTime NotificationDate { get; set; }
        public string NotificationMessage { get; set; }
        public string NotificationRecipients { get; set; } 

        public virtual NotificationModel Notification { get; set; } 
    }
}
