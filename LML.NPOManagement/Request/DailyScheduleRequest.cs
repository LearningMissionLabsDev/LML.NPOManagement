namespace LML.NPOManagement.Request
{
    public class DailyScheduleRequest
    {
        public WeeklyScheduleEnum WeeklyScheduleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
