using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{ 
    public class InvestorInformationRequest
    {
        [Required]
        [Range(1,int.MaxValue)]
        public int Id { get; set; } 

    }
}
