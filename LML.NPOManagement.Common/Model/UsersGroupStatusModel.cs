namespace LML.NPOManagement.Common.Model
{
    public class UsersGroupStatusModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public virtual ICollection<UsersGroupModel> UsersGroups { get; } = new List<UsersGroupModel>();
    }
}
