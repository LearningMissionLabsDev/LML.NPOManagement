using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class TemplateRequest
    {
        [Required]
        [Range(int.MinValue, int.MaxValue)]
        public int TemplateTypeId { get; set; }
        public byte[] Uri { get; set; } 

    }
}
