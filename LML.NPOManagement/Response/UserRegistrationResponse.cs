namespace LML.NPOManagement.Response
{
    public class UserRegistrationResponse
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
       
    }
}
