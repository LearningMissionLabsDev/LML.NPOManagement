﻿using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class Notification
    {
        public Notification()
        {
            NotificationTransportTypes = new HashSet<NotificationTransportType>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public int NotificationTypeId { get; set; }
        public int MeetingScheduleId { get; set; }
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public int AttachmentId { get; set; }
        public string Metadate { get; set; } = null!;
        public string? Reminder { get; set; }

        public virtual Attachment Attachment { get; set; } = null!;
        public virtual MeetingSchedule MeetingSchedule { get; set; } = null!;
        public virtual NotificationType NotificationType { get; set; } = null!;

        public virtual ICollection<NotificationTransportType> NotificationTransportTypes { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
