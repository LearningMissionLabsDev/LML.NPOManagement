using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IWeeklyScheduleService
    {
        public IEnumerable<WeeklyScheduleModel> GetAllWeeklyDay();
        public WeeklyScheduleModel GetWeeklySheduleByDay(int id);
        public int AddWeeklyShedule(WeeklyScheduleModel weeklyScheduleModel);
        public int ModifyWeeklyShedule(WeeklyScheduleModel weeklyScheduleModel, int id);
        public void DeleteWeeklyShedule(int id);
    }
}
