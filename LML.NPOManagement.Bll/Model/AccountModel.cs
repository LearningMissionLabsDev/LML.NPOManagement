
namespace LML.NPOManagement.Bll.Model
{
    public class AccountModel
    {
        public AccountModel()
        {
            AccountManagerInfos = new HashSet<AccountManagerInfoModel>();
            Beneficiaries = new HashSet<BeneficiaryModel>();
        }

        public int Id { get; set; }
        public string AccountName { get; set; } = null!;
        public string AccountDescription { get; set; }

        public virtual ICollection<AccountManagerInfoModel> AccountManagerInfos { get; set; }
        public virtual ICollection<BeneficiaryModel> Beneficiaries { get; set; }

    }
}
