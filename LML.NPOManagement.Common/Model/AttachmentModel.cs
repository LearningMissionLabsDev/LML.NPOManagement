namespace LML.NPOManagement.Common
{
    public class AttachmentModel
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public byte[] AttachmentData { get; set; } = null!;
        public virtual ICollection<NotificationModel> Notifications { get; } = new List<NotificationModel>();
    }
}
