namespace LML.NPOManagement.Common
{
    public class NotificationTypeModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public string Uri { get; set; } = null!;
        public virtual ICollection<NotificationModel> Notifications { get; } = new List<NotificationModel>();
    }
}
