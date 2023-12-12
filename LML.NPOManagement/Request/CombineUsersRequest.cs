namespace LML.NPOManagement.Request
{
    public class CombineUsersRequest
    {
        public UsersGroupRequest UsersGroupRequest { get; set; }
        public List<int> UserIds { get; set; }
    }
}
