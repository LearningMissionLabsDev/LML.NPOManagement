namespace LML.NPOManagement.Request
{
    public class BeneficiaryRequest
    {
        public int BeneficiaryRoleId { get; set; }
        public int StatusId { get; set; }
        public int BeneficiaryCategoryId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Gender { get; set; } = null!;

    }
}
