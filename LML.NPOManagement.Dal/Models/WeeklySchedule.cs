namespace LML.NPOManagement.Dal.Models
{
    public partial class WeeklySchedule
    {
        public WeeklySchedule()
        {
            DailySchedules = new HashSet<DailySchedule>();
            MeetingSchedules = new HashSet<MeetingSchedule>();
        }

        public int Id { get; set; }
        public string DayOfWeek { get; set; } = null!;
        public virtual ICollection<DailySchedule> DailySchedules { get; set; }
        public virtual ICollection<MeetingSchedule> MeetingSchedules { get; set; }
    }
}
