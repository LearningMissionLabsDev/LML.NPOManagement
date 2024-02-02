using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class InvestorInformation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int InvestorTierId { get; set; }
    public virtual ICollection<Donation> Donations { get; } = new List<Donation>();
    public virtual InvestorTierType InvestorTier { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
