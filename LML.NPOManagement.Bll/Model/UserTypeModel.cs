using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class UserTypeModel
    {
        public UserTypeModel()
        {
            Users = new HashSet<UserModel>();
        }
        public UserTypeModel(UserType userType)
        {
            userType.Id = Id;
            userType.Description = Description;
        }
        public int Id { get; set; }
        public string Description { get; set; } 

        public virtual ICollection<UserModel> Users { get; set; }
    }
}
