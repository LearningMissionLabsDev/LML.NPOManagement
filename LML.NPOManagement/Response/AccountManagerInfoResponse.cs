namespace LML.NPOManagement.Response
{
    public class AccountManagerInfoResponse
    {
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public int AccountManagerCategoryId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Information { get; set; }

        public virtual AccountManagerResponse AccountManagerCategoryRes { get; set; } = null!;
        public virtual RoleResponse RoleRes { get; set; } = null!;
        public virtual StatusResponse StatusRes { get; set; } = null!;

        public virtual ICollection<BeneficiaryResponse> BeneficiriesRes { get; set; }
    }
}
