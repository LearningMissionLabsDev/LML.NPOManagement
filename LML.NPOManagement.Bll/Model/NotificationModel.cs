namespace LML.NPOManagement.Bll.Model
{
    public class NotificationModel 
    {
        public NotificationModel()
        {
            NotificationArchives = new HashSet<NotificationArchiveModel>();
            NotificationTransportTypes = new HashSet<NotificationTransportTypeModel>();
            Users = new HashSet<UserModel>();
        }

        public int Id { get; set; }
        public NotificationTypeEnum NotificationTypeEnum  { get; set; }
        public int MeetingSchedule { get; set; }
        public string Subject { get; set; } 
        public string Body { get; set; } 
        public int AttachmentId { get; set; }
        public string Metadata { get; set; } 
        public string Reminder { get; set; }

        public virtual AttachmentModel Attachment { get; set; } 
        public virtual NotificationTypeModel NotificationType { get; set; } 
        public virtual ICollection<NotificationArchiveModel> NotificationArchives { get; set; }

        public virtual ICollection<NotificationTransportTypeModel> NotificationTransportTypes { get; set; }
        public virtual ICollection<UserModel> Users { get; set; }
    }
}
