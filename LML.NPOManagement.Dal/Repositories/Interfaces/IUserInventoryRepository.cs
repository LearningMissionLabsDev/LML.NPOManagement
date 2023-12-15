using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IUserInventoryRepository
    {
        DbSet<UserInventory> UserInventories { get; set; }
        DbSet<InventoryType> InventoryTypes { get; set; }
        Task<int> SaveChangesAsync();
        void SaveChanges();
    }
}
