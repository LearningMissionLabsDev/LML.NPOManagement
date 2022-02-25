using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class UserInventoryResponse
    {
        public int Id { get; set; }
        public int InventoryTypeId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Metadate { get; set; }
        public string Description { get; set; }

        public virtual InventoryTypeResponse InventoryType { get; set; } 
        public virtual UserResponse User { get; set; }
    }
}
