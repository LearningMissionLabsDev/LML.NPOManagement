using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class UserInventory
    {
        public int Id { get; set; }
        public int InventoryTypeId { get; set; }
        public int? UserId { get; set; }
        public DateTime Date { get; set; }
        public string Metadata { get; set; } = null!;
        public int Status { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public string? MeasurmentUnit { get; set; }
        public string? Description { get; set; }

        public virtual InventoryType InventoryType { get; set; } = null!;
        public virtual User? User { get; set; }
    }
}
