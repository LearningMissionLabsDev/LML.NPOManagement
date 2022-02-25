using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{ 
    public class TemplateTypeResponse
    {
        public TemplateTypeResponse()
        {
            Templates = new HashSet<TemplateResponse>();
        }

        public int Id { get; set; }
        public string Description { get; set; } 

        public virtual ICollection<TemplateResponse> Templates { get; set; }
    }
}
