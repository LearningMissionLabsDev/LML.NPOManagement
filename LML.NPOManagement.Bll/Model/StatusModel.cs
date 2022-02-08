
namespace LML.NPOManagement.Bll.Model
{
    public class StatusModel
    {
        public StatusModel()
        {
            AccountManagerInfos = new HashSet<AccountManagerInfoModel>();
            Beneficiaries = new HashSet<BeneficiaryModel>();
        }

        public int Id { get; set; }
        public ActivityStatus ActivityStatusType  { get; set; } 

        public virtual ICollection<AccountManagerInfoModel> AccountManagerInfos { get; set; }
        public virtual ICollection<BeneficiaryModel> Beneficiaries { get; set; }
    }
}
