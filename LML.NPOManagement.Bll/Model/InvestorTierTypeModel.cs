﻿using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class InvestorTierTypeModel
    {
        public InvestorTierTypeModel()
        {
            InvestorInformations = new HashSet<InvestorInformationModel>();
        }
        public InvestorTierTypeModel(InvestorTierType investorTier)
        {
            investorTier.Id = Id;
            investorTier.InvestorTier = InvestorTier;
        }
        public int Id { get; set; }
        public string InvestorTier { get; set; } 

        public virtual ICollection<InvestorInformationModel> InvestorInformations { get; set; }
    }
}
