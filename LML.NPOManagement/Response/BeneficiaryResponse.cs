namespace LML.NPOManagement.Response
{
    public class BeneficiaryResponse
    {
        public int BeneficiaryRoleId { get; set; }
        public int StatusId { get; set; }
        public int BeneficiaryCategoryId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;

        public virtual AccountResponse BeneficiaryCategory { get; set; } = null!;
        public virtual BeneficiaryRoleResponse BeneficiaryRole { get; set; } = null!;
        public virtual StatusResponse Status { get; set; } = null!;
    }
}
