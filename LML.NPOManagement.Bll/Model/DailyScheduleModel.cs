
namespace LML.NPOManagement.Bll.Model
{
    public class DailyScheduleModel
    {
        public int Id { get; set; }
        public  WeeklyScheduleEnum WeeklyScheduleEnum { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public virtual WeeklyScheduleModel WeeklySchedule { get; set; } 
    }
}
