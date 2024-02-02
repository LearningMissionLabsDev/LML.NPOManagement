namespace LML.NPOManagement.Common
{
    public class NotificationTypeModel
    {
        public NotificationTypeModel()
        {
            Notifications = new HashSet<NotificationModel>();
        }
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public string Uri { get; set; } = null!;
        public NotificationTypeEnum NotificationTypeEnum { get; set; }
        public virtual ICollection<NotificationModel> Notifications { get; } = new List<NotificationModel>();
    }
}
