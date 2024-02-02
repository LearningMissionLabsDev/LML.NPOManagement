using LML.NPOManagement.Common;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{ 
    public class UserInventoryRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int InventoryTypeId { get; set; }

        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(int.MaxValue)]
        public string Metadata { get; set; }
        public StatusEnumModel Status { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public string MeasurmentUnit { get; set; }

        [StringLength(int.MaxValue)]
        public string Description { get; set; }
    }
}
