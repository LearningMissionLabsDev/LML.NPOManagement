using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class NotificationRequest
    {        
        [Required]
        [StringLength (100)]
        public string Subject { get; set; }  
        
        [StringLength (maximumLength:int.MaxValue)]
        public string Metadate { get; set; } 

        [DataType (DataType.Time)]
        public string Reminder { get; set; }

    }
}
