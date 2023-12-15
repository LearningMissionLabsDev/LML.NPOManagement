using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IUserGroupRepository
    {
        DbSet<UsersGroup> UsersGroups { get; set; }
        Task<int> SaveChangesAsync();
        void SaveChanges();
    }
}
