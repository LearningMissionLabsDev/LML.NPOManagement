namespace LML.NPOManagement.Response
{
    public class UserIdeaResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? IdeaCategory { get; set; }
        public string IdeaDescription { get; set; } = null!;
    }
}
