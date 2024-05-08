using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class Attachment
{
    public int Id { get; set; }

    public int NotificationId { get; set; }

    public byte[] AttachmentData { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();
}
