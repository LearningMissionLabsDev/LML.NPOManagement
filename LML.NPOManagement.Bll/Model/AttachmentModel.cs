using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Bll.Model
{
    public class AttachmentModel
    {
        public AttachmentModel()
        {
            Notifications = new HashSet<NotificationModel>();
        }

        public int Id { get; set; }
        public int NotificationId { get; set; }
        public byte[] AttachmentData { get; set; } 

        public virtual ICollection<NotificationModel> Notifications { get; set; }
    }
}
