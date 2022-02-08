namespace LML.NPOManagement.Response
{
    public class AccountManagerInfoResponse
    {
        public int AccountManagerInfoRoleId { get; set; }
        public int StatusId { get; set; }
        public int AccountManagerCategoryId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;

        public virtual AccountResponse AccountManagerCategory { get; set; } = null!;
        public virtual AccountManagerRoleResponse AccountManagerCategory1 { get; set; } = null!;
        public virtual StatusResponse Status { get; set; } = null!;
    }
}
