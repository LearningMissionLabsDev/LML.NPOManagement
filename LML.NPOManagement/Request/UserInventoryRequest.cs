using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Request
{ 
    public class UserInventoryRequest
    {

        public int InventoryTypeId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Metadate { get; set; }
        public string Description { get; set; }

    }
}
