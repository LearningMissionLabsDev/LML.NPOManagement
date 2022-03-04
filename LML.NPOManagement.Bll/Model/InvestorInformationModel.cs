using LML.NPOManagement.Dal.Models;

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
        public InvestorTierTypeModel InvestorTierId { get; set; }

        public virtual InvestorTierTypeModel InvestorTier { get; set; }
        public virtual UserModel User { get; set; }
        public virtual ICollection<DonationModel> Donations { get; set; }
    }
}
