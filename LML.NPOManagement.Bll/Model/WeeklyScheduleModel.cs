using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Bll.Model
{
    public class WeeklyScheduleModel
    {
        public WeeklyScheduleModel()
        {
            DailySchedules = new HashSet<DailyScheduleModel>();
            MeetingSchedules = new HashSet<MeetingScheduleModel>();
        }

        public int Id { get; set; }
        public WeeklyScheduleEnum weeklySchedule { get; set; } 

        public virtual ICollection<DailyScheduleModel> DailySchedules { get; set; }
        public virtual ICollection<MeetingScheduleModel> MeetingSchedules { get; set; }
    }
}
