using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        DbSet<Attachment> Attachments { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<NotificationTransportType> NotificationTransportTypes { get; set; }
        DbSet<NotificationType> NotificationTypes { get; set; }
        Task<int> SaveChangesAsync();
        void SaveChanges();
    }
}
