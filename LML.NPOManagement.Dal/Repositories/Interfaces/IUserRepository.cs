using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IUserRepository
    {
        DbSet<User> Users { get; set; }
        DbSet<UserInformation> UserInformations { get; set; }
        DbSet<UserType> UserTypes { get; set; }
        DbSet<Role> Roles { get; set; }
        Task<int> SaveChangesAsync();
        void SaveChanges();
    }
}
