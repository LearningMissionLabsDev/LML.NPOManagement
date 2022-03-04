using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class MeetingScheduleModel
    {
        public MeetingScheduleModel()
        {
            Notifications = new HashSet<NotificationModel>();
        }

        public int Id { get; set; }
        public int WeeklyScheduleId { get; set; }
        public int DayOfMonth { get; set; }
        public DateTime DateInYear { get; set; }
        public int NumberOfMeeting { get; set; }

        public virtual WeeklyScheduleModel WeeklySchedule { get; set; }
        public virtual ICollection<NotificationModel> Notifications { get; set; }
    }
}
