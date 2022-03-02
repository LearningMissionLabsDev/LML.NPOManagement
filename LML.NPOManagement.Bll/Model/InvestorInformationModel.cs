using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model 
{
    public class InvestorInformationModel
    {
        public InvestorInformationModel()
        {
            Donations = new HashSet<DonationModel>();
        }
        public InvestorInformationModel(InvestorInformation investorInformation)
        {
            investorInformation.Id = Id;
            investorInformation.UserId = UserId;
            investorInformation.InvestorTierId = (int)InvestorTierId;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public InvestorTier InvestorTierId { get; set; }

        public virtual InvestorTierTypeModel InvestorTier { get; set; }
        public virtual UserModel User { get; set; }
        public virtual ICollection<DonationModel> Donations { get; set; }
    }
}
