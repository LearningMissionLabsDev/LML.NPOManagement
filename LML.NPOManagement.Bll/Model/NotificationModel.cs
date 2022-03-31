using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class NotificationModel : UserModel
    {
        public NotificationModel()
        {
            NotificationTypes = new HashSet<NotificationTypeModel>();
            Users = new HashSet<UserModel>();
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
