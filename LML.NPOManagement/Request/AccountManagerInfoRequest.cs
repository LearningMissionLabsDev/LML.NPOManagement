namespace LML.NPOManagement.Request
{
    public class AccountManagerInfoRequest
    {
        public int AccountManagerInfoRoleId { get; set; }
        public int StatusId { get; set; }
        public int AccountManagerCategoryId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Gender { get; set; } = null!;

    }
}
