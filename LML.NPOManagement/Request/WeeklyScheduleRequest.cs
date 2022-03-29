using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class WeeklyScheduleRequest
    {
       [Required]
       [Range (int.MinValue, int.MaxValue)]
        public int Id { get; set; }

        public WeeklyScheduleEnum weeklySchedule { get; set; } 

    }
}
