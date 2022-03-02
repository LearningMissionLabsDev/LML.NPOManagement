using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class RoleModel
    {
        public RoleModel()
        {
            Users = new HashSet<UserModel>();
        }
        public RoleModel(Role role)
        {
            role.Id = Id;
            role.UserRole = UserRole;
        }
        public int Id { get; set; }
        public string UserRole { get; set; } 

        public virtual ICollection<UserModel> Users { get; set; }
    }
}
