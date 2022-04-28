namespace LML.NPOManagement.Dal.Models
{
    public partial class TemplateType
    {
        public TemplateType()
        {
            Templates = new HashSet<Template>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public virtual ICollection<Template> Templates { get; set; }
    }
}
