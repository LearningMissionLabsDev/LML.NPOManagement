using System;
using System.Collections.Generic;
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
        public string Metadate { get; set; }
        
        [StringLength(int.MaxValue)]
        public string Description { get; set; }

    }
}
