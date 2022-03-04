using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class NotificationResponse
    {
        public int MeetingScheduleId { get; set; }
        public string Subject { get; set; } 
        public string Body { get; set; } 
        public int AttachmentId { get; set; }
        public string Metadate { get; set; } 
        public string Reminder { get; set; }

    }
}
