using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IInvestorRepository
    {
        DbSet<Donation> Donations { get; set; }
        DbSet<InvestorInformation> InvestorInformations { get; set; }
        DbSet<InvestorTierType> InvestorTierTypes { get; set; }
        Task<int> SaveChangesAsync();
        void SaveChanges();
    }
}
