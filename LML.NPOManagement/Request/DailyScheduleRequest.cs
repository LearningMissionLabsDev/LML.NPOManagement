using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class DailyScheduleRequest
    {
        [Required]
        [DataType (DataType.Time)]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

    }
}
