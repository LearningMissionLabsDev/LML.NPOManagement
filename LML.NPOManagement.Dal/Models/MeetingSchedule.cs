using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class MeetingSchedule
    {
        public MeetingSchedule()
        {
            Notifications = new HashSet<Notification>();
        }

        public int Id { get; set; }
        public int? WeeklyScheduleId { get; set; }
        public int? DayOfMonth { get; set; }
        public DateTime? DateInYear { get; set; }
        public int NumberOfMeeting { get; set; }

        public virtual WeeklySchedule? WeeklySchedule { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
