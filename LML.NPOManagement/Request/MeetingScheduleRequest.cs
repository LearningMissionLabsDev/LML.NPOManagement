using LML.NPOManagement.Bll.Model;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class MeetingScheduleRequest
    {
        public WeeklyScheduleEnum weeklySchedule { get; set; }

        [Range(1,31)]
        public int DayOfMont { get; set; }

        [DataType (DataType.DateTime)]
        public DateTime DateInYear { get; set; }

        [Range (1, int.MaxValue)]
        public int NumberOfMeeting { get; set; }

    }
}
