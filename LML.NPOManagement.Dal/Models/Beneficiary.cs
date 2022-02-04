
namespace LML.NPOManagement.Dal.Models
{
    public partial class Beneficiary
    {
        public Beneficiary()
        {
            AccountManagerInfos = new HashSet<AccountManagerInfo>();
        }

        public int Id { get; set; }
        public int BeneficiaryRoleId { get; set; }
        public int StatusId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? BeneficiaryInfo { get; set; }

        public virtual BeneficiaryRole BeneficiaryRole { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;

        public virtual ICollection<AccountManagerInfo> AccountManagerInfos { get; set; }
    }
}
