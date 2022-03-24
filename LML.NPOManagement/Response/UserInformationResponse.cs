namespace LML.NPOManagement.Response
{
    public class UserInformationResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Token { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Metadata { get; set; }
    }
}
