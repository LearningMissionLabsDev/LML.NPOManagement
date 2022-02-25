using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class TemplateResponse
    {
        public int Id { get; set; }
        public int TemplateTypeId { get; set; }
        public byte[] Uri { get; set; } 

        public virtual TemplateTypeResponse TemplateType { get; set; } 
    }
}
