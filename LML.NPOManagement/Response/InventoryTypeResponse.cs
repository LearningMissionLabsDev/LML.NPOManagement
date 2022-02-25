using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class InventoryTypeResponse
    {
        public InventoryTypeResponse()
        {
            UserInventories = new HashSet<UserInventoryResponse>();
        }

        public int Id { get; set; }
        public string Description { get; set; } 

        public virtual ICollection<UserInventoryResponse> UserInventories { get; set; }
    }
}
