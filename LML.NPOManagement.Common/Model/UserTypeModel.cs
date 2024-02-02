namespace LML.NPOManagement.Common
{
    public class UserTypeModel
    {
        public UserTypeModel()
        {
            Users = new HashSet<UserModel>();
        }
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public virtual ICollection<UserModel> Users { get; } = new List<UserModel>();
    }
}
