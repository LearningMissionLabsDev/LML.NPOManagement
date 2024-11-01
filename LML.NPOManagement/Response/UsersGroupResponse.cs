namespace LML.NPOManagement.Response
{
    public class UsersGroupResponse
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string GroupName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
