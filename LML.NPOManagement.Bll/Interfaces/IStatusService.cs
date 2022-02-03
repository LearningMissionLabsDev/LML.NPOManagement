using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IStatusService
    {
        public IEnumerable<StatusModel> GetAllStatus();
        public StatusModel GetStatusById(int id);
    }
}
