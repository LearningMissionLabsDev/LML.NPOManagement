namespace LML.NPOManagement.Response
{
    public class DailyScheduleResponse
    {
        public WeeklyScheduleEnum WeeklyScheduleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
