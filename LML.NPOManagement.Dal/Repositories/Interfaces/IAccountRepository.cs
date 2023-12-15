using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<AccountProgress> AccountProgresses { get; set; }
        DbSet<UserIdea> UserIdeas { get; set; }
        Task<int> SaveChangesAsync();
        void SaveChanges();
    }
}
