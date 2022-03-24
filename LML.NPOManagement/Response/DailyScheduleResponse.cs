namespace LML.NPOManagement.Response
{
    public class DailyScheduleResponse
    {
        public int Id { get; set; }
        public WeeklyScheduleEnum WeeklyScheduleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
