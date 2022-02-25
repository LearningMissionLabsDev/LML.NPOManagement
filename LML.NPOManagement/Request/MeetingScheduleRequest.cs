using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Request
{
    public class MeetingScheduleRequest
    {
        public WeeklyScheduleEnum weeklySchedule { get; set; }
        public int DayOfMont { get; set; }
        public DateTime DateInYear { get; set; }
        public int NumberOfMeeting { get; set; }

    }
}
