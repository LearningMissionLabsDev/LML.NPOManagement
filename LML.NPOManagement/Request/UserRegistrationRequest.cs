namespace LML.NPOManagement.Request
{
    public class UserRegistrationRequest
    {
        public UserRequest UserRequest { get; set; }
        public UserTypeRequest UserTypeRequest { get; set; }
        public UserInformationRequest UserInformationRequest { get; set; }
    }
}
