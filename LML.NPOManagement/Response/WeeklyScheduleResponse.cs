using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class WeeklyScheduleResponse
    {
        public int Id { get; set; }
        public WeeklyScheduleEnum WeeklySchedule { get; set; } 
    }
}
