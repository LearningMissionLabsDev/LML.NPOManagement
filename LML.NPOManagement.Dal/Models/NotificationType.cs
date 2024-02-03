﻿using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class NotificationType
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public string Uri { get; set; } = null!;
    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();
}
