using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface ITemplateTypeService
    {
        public IEnumerable<TemplateTypeModel> GetAllTemplateTypes();
        public TemplateTypeModel GetTemplateTypeById(int id);
        public int AddTemplateType(TemplateTypeModel templateTypeModel);
        public int ModifyTemplateType(TemplateTypeModel templateTypeModel, int id);
        public void DeleteTemplateType(int id);
    }
}
