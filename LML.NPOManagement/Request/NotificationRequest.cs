using LML.NPOManagement.Bll.Model;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class NotificationRequest
    {
        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        public NotificationTypeEnum NotificationTypeEnum { get; set; }

        public NotificationContext NotificationContext { get; set; }

        public string Body { get; set; }

        [DataType(DataType.Time)]
        public string Reminder { get; set; }

    }
}
