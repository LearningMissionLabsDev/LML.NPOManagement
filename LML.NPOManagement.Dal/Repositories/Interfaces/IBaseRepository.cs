
namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        Task<int> SaveChangesAsync();
    }
}
