namespace LML.NPOManagement.Dal.Models
{
    public partial class InvestorTierType
    {
        public InvestorTierType()
        {
            InvestorInformations = new HashSet<InvestorInformation>();
        }

        public int Id { get; set; }
        public string InvestorTier { get; set; } = null!;
        public virtual ICollection<InvestorInformation> InvestorInformations { get; set; }
    }
}
