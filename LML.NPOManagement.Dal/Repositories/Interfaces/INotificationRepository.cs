﻿using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        DbSet<Attachment> Attachments { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<NotificationTransportType> NotificationTransportTypes { get; set; }
        DbSet<NotificationType> NotificationTypes { get; set; }
    }
}
