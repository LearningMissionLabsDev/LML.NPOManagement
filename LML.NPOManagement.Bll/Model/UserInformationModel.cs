using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class UserInformationModel
    {
        public UserInformationModel()
        {
            Users = new HashSet<UserModel>();
        }
        public UserInformationModel(UserInformation userInformation)
        {
            userInformation.Id = Id;
            userInformation.FirstName = FirstName;
            userInformation.LastName = LastName;
            userInformation.MiddleName = MiddleName;
            userInformation.DateOfBirth = DateOfBirth;
            userInformation.CreateDate = CreateDate;
            userInformation.UpdateDate = UpdateDate;
            userInformation.PhoneNumber = PhoneNumber;
            userInformation.Gender = Gender;
            userInformation.Information = UserInfo;
        }
        public int Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string PhoneNumber { get; set; } 
        public int Gender { get; set; }
        public string UserInfo { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }
    }
}
