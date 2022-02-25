using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class Template
    {
        public int Id { get; set; }
        public int TemplateTypeId { get; set; }
        public byte[] Uri { get; set; } = null!;

        public virtual TemplateType TemplateType { get; set; } = null!;
    }
}
