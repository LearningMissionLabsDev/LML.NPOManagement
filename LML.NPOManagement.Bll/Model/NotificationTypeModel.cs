namespace LML.NPOManagement.Bll.Model
{
    public class NotificationTypeModel
    {
        public NotificationTypeModel()
        {
            Notifications = new HashSet<NotificationModel>();
        }

        public int Id { get; set; }
        public NotificationTypeEnum NotificationTypeEnum { get; set; } 
        public virtual ICollection<NotificationModel> Notifications { get; set; }
    }
}
