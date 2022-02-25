using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class InvestorTierResponse
    {
        public InvestorTierResponse()
        {
            InvestorInformations = new HashSet<InvestorInformationResponse>();
        }

        public int Id { get; set; }
        public string InvestorTierInfo { get; set; } 

        public virtual ICollection<InvestorInformationResponse> InvestorInformations { get; set; }
    }
}
