﻿using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class InvestorInformation
    {
        public InvestorInformation()
        {
            Donations = new HashSet<Donation>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? InvestorTierId { get; set; }

        public virtual InvestorTierType? InvestorTier { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }
    }
}
