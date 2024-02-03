namespace LML.NPOManagement.Common
{
    public class InvestorTierTypeModel
    {
        public InvestorTierTypeModel()
        {
            InvestorInformations = new HashSet<InvestorInformationModel>();
        }
        public int Id { get; set; }
        public string InvestorTier { get; set; } = null!;
        public virtual ICollection<InvestorInformationModel> InvestorInformations { get; } = new List<InvestorInformationModel>();
    }
}
