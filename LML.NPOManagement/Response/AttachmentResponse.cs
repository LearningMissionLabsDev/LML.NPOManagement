using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class AttachmentResponse
    {
        public AttachmentResponse()
        {
            Notifications = new HashSet<NotificationResponse>();
        }

        public int Id { get; set; }
        public int NotificationId { get; set; }
        public byte[] AttachmentData { get; set; } 

        public virtual ICollection<NotificationResponse> Notifications { get; set; }
    }
}
