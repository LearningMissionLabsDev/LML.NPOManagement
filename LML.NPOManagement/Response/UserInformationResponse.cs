namespace LML.NPOManagement.Response
{
    public class UserInformationResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Token { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
