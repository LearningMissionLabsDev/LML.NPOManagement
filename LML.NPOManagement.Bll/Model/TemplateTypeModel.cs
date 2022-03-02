using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model 
{ 
    public class TemplateTypeModel
    {
        public TemplateTypeModel()
        {
            Templates = new HashSet<TemplateModel>();
        }
        public TemplateTypeModel(TemplateType templateType)
        {
            templateType.Id = Id;
            templateType.Description = Description;
        }
        public int Id { get; set; }
        public string Description { get; set; } 

        public virtual ICollection<TemplateModel> Templates { get; set; }
    }
}
