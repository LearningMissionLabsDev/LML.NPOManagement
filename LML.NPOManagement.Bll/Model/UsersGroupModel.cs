namespace LML.NPOManagement.Bll.Model
{
    public partial class UsersGroupModel
    {
        public UsersGroupModel()
        {
            Users = new HashSet<UserModel>();
        }
        public int Id { get; set; }
        public int CreatedByUserId { get; set; }
        public string GroupName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string? Description { get; set; }
        public virtual UserModel CreatedByUser { get; set; } = null!;
        public virtual ICollection<UserModel> Users { get; set; }
    }
}
