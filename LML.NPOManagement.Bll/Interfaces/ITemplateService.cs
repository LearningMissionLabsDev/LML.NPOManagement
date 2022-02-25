using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface ITemplateService
    {
        public IEnumerable<TemplateModel> GetAllTemplates();
        public TemplateModel GetTemplateById(int id);
        public int AddTemplate(TemplateModel templateModel);
        public int ModifyTemplate(TemplateModel templateModel, int id);
        public void DeleteTemplate(int id);
    }
}
