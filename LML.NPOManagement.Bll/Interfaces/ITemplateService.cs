using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface ITemplateService
    {
        public string RegistrationUser(string name, DateTime createDate);
    }
}
