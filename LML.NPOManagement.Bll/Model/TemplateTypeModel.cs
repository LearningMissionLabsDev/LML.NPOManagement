using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Bll.Model 
{ 
    public class TemplateTypeModel
    {
        public TemplateTypeModel()
        {
            Templates = new HashSet<TemplateModel>();
        }

        public int Id { get; set; }
        public string Description { get; set; } 

        public virtual ICollection<TemplateModel> Templates { get; set; }
    }
}
