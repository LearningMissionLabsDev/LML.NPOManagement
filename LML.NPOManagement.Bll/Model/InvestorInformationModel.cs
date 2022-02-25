using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Bll.Model 
{ 
    public class InvestorInformationModel
    {
        public InvestorInformationModel()
        {
            Donations = new HashSet<DonationModel>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int InvestorTierId { get; set; }

        public virtual InvestorTierModel InvestorTier { get; set; }
        public virtual UserModel User { get; set; }
        public virtual ICollection<DonationModel> Donations { get; set; }
    }
}
