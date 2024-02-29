using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class NotificationTransportType
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();
}
