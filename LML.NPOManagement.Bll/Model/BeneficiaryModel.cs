
namespace LML.NPOManagement.Bll.Model
{
    public class BeneficiaryModel
    {
        public BeneficiaryModel()
        {
            BeneficiaryInventories = new HashSet<BeneficiaryInventoryModel>();
        }

        public int Id { get; set; }
        public int BeneficiaryRoleId { get; set; }
        public ActivityStatus ActivityStatusType { get; set; }
        public int BeneficiaryCategoryId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public GenderModel Gender { get; set; } 
        public string BeneficiaryInfo { get; set; }

        public virtual AccountModel BeneficiaryCategory { get; set; } = null!;
        public virtual BeneficiaryRoleModel BeneficiaryRole { get; set; } = null!;
        public virtual StatusModel Status { get; set; } = null!;
        public virtual ICollection<BeneficiaryInventoryModel> BeneficiaryInventories { get; set; }
    }
}
