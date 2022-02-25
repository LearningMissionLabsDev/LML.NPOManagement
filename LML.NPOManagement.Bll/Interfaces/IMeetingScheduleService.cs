using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IMeetingScheduleService
    {
        public IEnumerable<MeetingScheduleModel> GetAllMeetingSchedules();
        public MeetingScheduleModel GetMeetingScheduleById(int id);
        public int AddMeetingSchedule(MeetingScheduleModel meetingScheduleModel);
        public int ModifyMeetingSchedule(MeetingScheduleModel meetingScheduleModel, int id);
        public void DeleteMeetingSchedule(int id);
    }
}
