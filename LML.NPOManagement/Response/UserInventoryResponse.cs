﻿using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{ 
    public class UserInventoryResponse
    {
        public int InventoryTypeId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Metadate { get; set; }
    }
}
