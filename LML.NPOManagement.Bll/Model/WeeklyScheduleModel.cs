using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class WeeklyScheduleModel
    {
        public WeeklyScheduleModel()
        {
            DailySchedules = new HashSet<DailyScheduleModel>();
            MeetingSchedules = new HashSet<MeetingScheduleModel>();
        }

        public int Id { get; set; }
        public WeeklyScheduleEnum WeeklyScheduleEnum { get; set; } 

        public virtual ICollection<DailyScheduleModel> DailySchedules { get; set; }
        public virtual ICollection<MeetingScheduleModel> MeetingSchedules { get; set; }
    }
}
