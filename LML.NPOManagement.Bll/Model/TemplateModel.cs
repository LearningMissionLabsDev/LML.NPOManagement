using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class TemplateModel
    {
        public TemplateModel(Template template)
        {
            template.Id = Id;
            template.TemplateTypeId = TemplateTypeId;
            template.Uri = Uri;
        }
        public int Id { get; set; }
        public int TemplateTypeId { get; set; }
        public byte[] Uri { get; set; } 

        public virtual TemplateTypeModel TemplateType { get; set; } 
    }
}
