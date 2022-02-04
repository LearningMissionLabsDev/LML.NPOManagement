using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IStatusService
    {
        public IEnumerable<StatusModel> GetAllStatus();
        public StatusModel GetStatusById(int id);
    }
}
