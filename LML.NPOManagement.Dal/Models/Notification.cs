using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int NotificationTypeId { get; set; }

    public int MeetingSchedule { get; set; }

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public int AttachmentId { get; set; }

    public string Metadata { get; set; } = null!;

    public string? Reminder { get; set; }

    public virtual Attachment Attachment { get; set; } = null!;

    public virtual NotificationType NotificationType { get; set; } = null!;

    public virtual ICollection<NotificationTransportType> NotificationTransportTypes { get; } = new List<NotificationTransportType>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
