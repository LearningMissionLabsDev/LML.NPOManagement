using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class UserTypeModel
    {
        public UserTypeModel()
        {
            Users = new HashSet<UserModel>();
        }
        public int Id { get; set; }
        public UserTypeEnum UserTypeEnum { get; set; } 
        public virtual ICollection<UserModel> Users { get; set; }
    }
}
