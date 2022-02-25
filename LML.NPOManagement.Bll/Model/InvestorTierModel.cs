using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Bll.Model
{
    public class InvestorTierModel
    {
        public InvestorTierModel()
        {
            InvestorInformations = new HashSet<InvestorInformationModel>();
        }

        public int Id { get; set; }
        public string InvestorTierInfo { get; set; } 

        public virtual ICollection<InvestorInformationModel> InvestorInformations { get; set; }
    }
}
