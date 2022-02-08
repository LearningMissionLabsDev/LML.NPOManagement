
namespace LML.NPOManagement.Dal.Models
{
    public partial class Account
    {
        public Account()
        {
            AccountManagerInfos = new HashSet<AccountManagerInfo>();
            Beneficiaries = new HashSet<Beneficiary>();
        }

        public int Id { get; set; }
        public string AccountName { get; set; } = null!;
        public string? AccountDescription { get; set; }

        public virtual ICollection<AccountManagerInfo> AccountManagerInfos { get; set; }
        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }
    }
}
