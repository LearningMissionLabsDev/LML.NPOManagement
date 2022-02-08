using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Response
{
    public class BeneficiaryResponse
    {
        public int BeneficiaryRoleId { get; set; }
        public ActivityStatus ActivityStatusType { get; set; }
        public int BeneficiaryCategoryId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public GenderModel Gender { get; set; }

        public virtual AccountResponse BeneficiaryCategoryRes { get; set; } = null!;
        public virtual BeneficiaryRoleResponse BeneficiaryRoleRes { get; set; } = null!;
        public virtual StatusResponse StatusRes { get; set; } = null!;
        public virtual ICollection<BeneficiaryInventoryResponse> BeneficiaryInventoriesRes { get; set; }
    }
}
