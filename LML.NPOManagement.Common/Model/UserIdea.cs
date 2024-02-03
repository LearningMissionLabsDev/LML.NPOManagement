
namespace LML.NPOManagement.Common
{
    public partial class UserIdeaModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? IdeaCategory { get; set; }
        public string IdeaDeskcription { get; set; } 

        public virtual UserModel User { get; set; } 
    }
}
