using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IDailyScheduleService
    {
        public IEnumerable<DailyScheduleModel> GetAllDailySchedules();
        public DailyScheduleModel GetDailyScheduleById(int id);
        public int AddDailySchedule(DailyScheduleModel dailyScheduleModel);
        public int ModifyDailySchedule(DailyScheduleModel dailyScheduleModel, int id);
        public void DeleteDailySchedule(int id);
    }
}
