using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class WeeklyScheduleResponse
    {
        public WeeklyScheduleResponse()
        {
            DailySchedules = new HashSet<DailyScheduleResponse>();
            MeetingSchedules = new HashSet<MeetingScheduleResponse>();
        }

        public int Id { get; set; }
        public WeeklyScheduleEnum weeklySchedule { get; set; } 

        public virtual ICollection<DailyScheduleResponse> DailySchedules { get; set; }
        public virtual ICollection<MeetingScheduleResponse> MeetingSchedules { get; set; }
    }
}
