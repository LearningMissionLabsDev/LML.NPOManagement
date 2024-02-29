
namespace LML.NPOManagement.Common.Model
{
    public class UserIdeaModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? IdeaCategory { get; set; }
        public string IdeaDescription { get; set; } = null!;
        public virtual UserModel User { get; set; } = null!;
    }
}
