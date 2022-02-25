using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Request
{
    public class DailyScheduleRequest
    {
        public  WeeklyScheduleEnum weeklySchedule { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
