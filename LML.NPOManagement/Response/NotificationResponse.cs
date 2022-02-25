using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class NotificationResponse
    {
        public NotificationResponse()
        {
            NotificationTypes = new HashSet<NotificationTypeResponse>();
            Users = new HashSet<UserResponse>();
        }

        public int Id { get; set; }
        public int MeetingScheduleId { get; set; }
        public string Subject { get; set; } 
        public string Body { get; set; } 
        public int AttachmentId { get; set; }
        public string Metadate { get; set; } 
        public string Reminder { get; set; }

        public virtual AttachmentResponse Attachment { get; set; } 
        public virtual MeetingScheduleResponse MeetingSchedule { get; set; } 

        public virtual ICollection<NotificationTypeResponse> NotificationTypes { get; set; }
        public virtual ICollection<UserResponse> Users { get; set; }
    }
}
