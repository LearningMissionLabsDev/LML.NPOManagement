using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class DailyScheduleModel
    {
        public DailyScheduleModel(DailySchedule dailySchedule)
        {
            dailySchedule.Id = Id;
            dailySchedule.WeeklyScheduleId =(int) WeeklyScheduleId;
            dailySchedule.StartTime = StartTime;
            dailySchedule.EndTime = EndTime;
        }
        public int Id { get; set; }
        public  WeeklyScheduleEnum WeeklyScheduleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual WeeklyScheduleModel WeeklySchedule { get; set; } 
    }
}
