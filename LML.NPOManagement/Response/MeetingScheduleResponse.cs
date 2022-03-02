using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class MeetingScheduleResponse
    {
        public MeetingScheduleResponse()
        {
            Notifications = new HashSet<NotificationResponse>();
        }

        public int Id { get; set; }
        public WeeklyScheduleEnum WeeklyScheduleId { get; set; }
        public int DayOfMonth { get; set; }
        public DateTime DateInYear { get; set; }
        public int NumberOfMeeting { get; set; }

        public virtual WeeklyScheduleResponse WeeklySchedule { get; set; }
        public virtual ICollection<NotificationResponse> Notifications { get; set; }
    }
}
