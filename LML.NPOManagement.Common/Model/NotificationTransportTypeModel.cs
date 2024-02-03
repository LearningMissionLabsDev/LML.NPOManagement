namespace LML.NPOManagement.Common
{
    public class NotificationTransportTypeModel
    {
        public NotificationTransportTypeModel()
        {
            Notifications = new HashSet<NotificationModel>();
        }
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public virtual ICollection<NotificationModel> Notifications { get; } = new List<NotificationModel>();
    }
}
