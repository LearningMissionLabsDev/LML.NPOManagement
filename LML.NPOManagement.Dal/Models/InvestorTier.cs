using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class InvestorTier
    {
        public InvestorTier()
        {
            InvestorInformations = new HashSet<InvestorInformation>();
        }

        public int Id { get; set; }
        public string InvestorTierType { get; set; } = null!;

        public virtual ICollection<InvestorInformation> InvestorInformations { get; set; }
    }
}
