using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class InvestorTierType
{
    public int Id { get; set; }

    public string InvestorTier { get; set; } = null!;

    public virtual ICollection<InvestorInformation> InvestorInformations { get; } = new List<InvestorInformation>();
}
