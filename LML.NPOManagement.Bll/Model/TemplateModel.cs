using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Bll.Model
{
    public class TemplateModel
    {
        public int Id { get; set; }
        public int TemplateTypeId { get; set; }
        public byte[] Uri { get; set; } 

        public virtual TemplateTypeModel TemplateType { get; set; } 
    }
}
