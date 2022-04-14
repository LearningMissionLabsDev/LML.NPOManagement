

namespace LML.NPOManagement.Bll.Model
{
    public class NotificationTransportTypeModel
    {
        public NotificationTransportTypeModel()
        {
            Notifications = new HashSet<NotificationModel>();
        }

        public int Id { get; set; }
        public string Description { get; set; } 
        public virtual ICollection<NotificationModel> Notifications { get; set; }
    }
}
