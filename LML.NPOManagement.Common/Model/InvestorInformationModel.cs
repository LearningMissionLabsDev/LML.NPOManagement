namespace LML.NPOManagement.Common 
{
    public class InvestorInformationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int InvestorTierId { get; set; }
        public virtual ICollection<DonationModel> Donations { get; } = new List<DonationModel>();
        public virtual InvestorTierTypeModel InvestorTier { get; set; } = null!;
        public virtual UserModel User { get; set; } = null!;
    }
}
