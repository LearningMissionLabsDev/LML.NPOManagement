using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class NotificationArchive
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public DateTime NotificationDate { get; set; }
        public string NotificationMessage { get; set; } = null!;
        public string NotificationRecipients { get; set; } = null!;

        public virtual Notification Notification { get; set; } = null!;
    }
}
