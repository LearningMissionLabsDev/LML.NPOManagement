using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Dal
{
    public interface INPOManagementContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<AccountProgress> AccountProgresses { get; set; } 
        DbSet<Attachment> Attachments { get; set; } 
        DbSet<Donation> Donations { get; set; } 
        DbSet<InventoryType> InventoryTypes { get; set; } 
        DbSet<InvestorInformation> InvestorInformations { get; set; } 
        DbSet<InvestorTierType> InvestorTierTypes { get; set; } 
        DbSet<Notification> Notifications { get; set; } 
        DbSet<NotificationTransportType> NotificationTransportTypes { get; set; }
        DbSet<NotificationType> NotificationTypes { get; set; } 
        DbSet<Role> Roles { get; set; } 
        DbSet<User> Users { get; set; } 
        DbSet<UserIdea> UserIdeas { get; set; } 
        DbSet<UserInformation> UserInformations { get; set; } 
        DbSet<UserInventory> UserInventories { get; set; } 
        DbSet<UserType> UserTypes { get; set; }
        DbSet<UsersGroup> UsersGroups { get; set; }
        Task<int> SaveChangesAsync();
        void SaveChanges();
    }
}
