using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class DailySchedule
    {
        public int Id { get; set; }
        public int WeeklyScheduleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual WeeklySchedule WeeklySchedule { get; set; } = null!;
    }
}
