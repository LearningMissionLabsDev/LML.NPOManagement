namespace LML.NPOManagement.Dal.Models
{
    public partial class NotificationTransportType
    {
        public NotificationTransportType()
        {
            Notifications = new HashSet<Notification>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
