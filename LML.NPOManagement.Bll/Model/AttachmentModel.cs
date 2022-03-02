using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class AttachmentModel
    {
        public AttachmentModel()
        {
            Notifications = new HashSet<NotificationModel>();
        }
        public AttachmentModel(Attachment attachment)
        {
            attachment.Id = Id;
            attachment.NotificationId = NotificationId;
            attachment.AttachmentData = AttachmentData;
        }
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public byte[] AttachmentData { get; set; } 

        public virtual ICollection<NotificationModel> Notifications { get; set; }
    }
}
