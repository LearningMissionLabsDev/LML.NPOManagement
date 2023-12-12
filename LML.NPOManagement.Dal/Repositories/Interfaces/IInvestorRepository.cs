using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IInvestorRepository
    {
        DbSet<Donation> Donations { get; set; }
        DbSet<InvestorInformation> InvestorInformations { get; set; }
        DbSet<InvestorTierType> InvestorTierTypes { get; set; }
    }
}
