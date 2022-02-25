using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Request
{
    public class NotificationRequest
    {
        public int MeetingScheduleId { get; set; }
        public string Subject { get; set; } 
        public string Body { get; set; } 
        public int AttachmentId { get; set; }
        public string Metadate { get; set; } 
        public string Reminder { get; set; }

    }
}
