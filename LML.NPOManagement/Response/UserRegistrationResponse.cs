namespace LML.NPOManagement.Response
{
    public class UserRegistrationResponse
    {
        public UserTypeResponse? UserTypeResponse { get; set; }
        public UserResponse UserResponse { get; set; }
        public UserInformationResponse UserInformation { get; set; }
        public RoleResponse RoleResponse { get; set; }
    }
}
