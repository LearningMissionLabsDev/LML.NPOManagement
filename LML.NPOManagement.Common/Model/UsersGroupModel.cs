using LML.NPOManagement.Common.Model;

namespace LML.NPOManagement.Common
{
    public partial class UsersGroupModel
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public int StatusId { get; set; }
        public string GroupName { get; set; } = null!;
        public List<int> UserIds { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string? Description { get; set; }
        public virtual UserModel Creator { get; set; } = null!;
        public virtual UsersGroupStatusModel Status { get; set; } = null!;
        public virtual ICollection<UserModel> Users { get; set; } = new List<UserModel>();
    }
}
