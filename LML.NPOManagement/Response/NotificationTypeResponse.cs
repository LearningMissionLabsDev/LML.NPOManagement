using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{ 
    public class NotificationTypeResponse
    {
        public NotificationTypeResponse()
        {
            Notifications = new HashSet<NotificationResponse>();
        }

        public int Id { get; set; }
        public string Description { get; set; } 

        public virtual ICollection<NotificationResponse> Notifications { get; set; }
    }
}
