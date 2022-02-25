using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class DailyScheduleResponse
    {
        public int Id { get; set; }
        public  WeeklyScheduleEnum weeklySchedule { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual WeeklyScheduleResponse WeeklySchedule { get; set; } 
    }
}
