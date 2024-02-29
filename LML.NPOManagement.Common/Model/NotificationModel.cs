using System.Net.Mail;

namespace LML.NPOManagement.Common
{
    public class NotificationModel 
    {
        public int Id { get; set; }
        public int NotificationTypeId { get; set; }
        public int MeetingSchedule { get; set; }
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public int AttachmentId { get; set; }
        public NotificationTypeEnum NotificationTypeEnum { get; set; }
        public string Metadata { get; set; } = null!;
        public string? Reminder { get; set; }
        public virtual Attachment Attachment { get; set; } = null!;
        public virtual NotificationTypeModel NotificationType { get; set; } = null!;
        public virtual ICollection<NotificationTransportTypeModel> NotificationTransportTypes { get; } = new List<NotificationTransportTypeModel>();
        public virtual ICollection<UserModel> Users { get; } = new List<UserModel>();
    }
}
