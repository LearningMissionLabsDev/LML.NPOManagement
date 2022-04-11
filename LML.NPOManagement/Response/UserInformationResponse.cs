namespace LML.NPOManagement.Response
{
    public class UserInformationResponse
    {

        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string? Metadata { get; set; }
    }
}
