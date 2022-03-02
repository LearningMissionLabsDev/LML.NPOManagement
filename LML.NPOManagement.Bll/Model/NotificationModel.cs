using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class NotificationModel
    {
        public NotificationModel()
        {
            NotificationTypes = new HashSet<NotificationTypeModel>();
            Users = new HashSet<UserModel>();
        }
        public NotificationModel(Notification notification)
        {
            notification.Id = Id;
            notification.MeetingScheduleId = MeetingScheduleId;
            notification.Subject = Subject;
            notification.Body = Body;
            notification.AttachmentId = AttachmentId;
            notification.Metadate = Metadate;
            notification.Reminder = Reminder;
        }
        public int Id { get; set; }
        public int MeetingScheduleId { get; set; }
        public string Subject { get; set; } 
        public string Body { get; set; } 
        public int AttachmentId { get; set; }
        public string Metadate { get; set; } 
        public string Reminder { get; set; }

        public virtual AttachmentModel Attachment { get; set; } 
        public virtual MeetingScheduleModel MeetingSchedule { get; set; } 

        public virtual ICollection<NotificationTypeModel> NotificationTypes { get; set; }
        public virtual ICollection<UserModel> Users { get; set; }
    }
}
