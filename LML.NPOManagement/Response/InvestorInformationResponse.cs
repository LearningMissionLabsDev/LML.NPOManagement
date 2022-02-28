using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{ 
    public class InvestorInformationResponse
    {
        public InvestorInformationResponse()
        {
            Donations = new HashSet<DonationResponse>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int InvestorTierId { get; set; }

        public virtual UserResponse User { get; set; }
        public virtual ICollection<DonationResponse> Donations { get; set; }
    }
}
