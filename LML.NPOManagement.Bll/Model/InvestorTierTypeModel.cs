namespace LML.NPOManagement.Bll.Model
{
    public class InvestorTierTypeModel
    {
        public InvestorTierTypeModel()
        {
            InvestorInformations = new HashSet<InvestorInformationModel>();
        }

        public int Id { get; set; }
        public InvestorTierEnum InvestorTierEnum { get; set; } 
        public virtual ICollection<InvestorInformationModel> InvestorInformations { get; set; }
    }
}
