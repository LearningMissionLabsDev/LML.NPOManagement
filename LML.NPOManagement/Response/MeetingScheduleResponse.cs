using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class MeetingScheduleResponse
    {
        public WeeklyScheduleEnum WeeklySchedule { get; set; }
        public int DayOfMont { get; set; }
        public DateTime DateInYear { get; set; }
        public int NumberOfMeeting { get; set; }

    }
}
