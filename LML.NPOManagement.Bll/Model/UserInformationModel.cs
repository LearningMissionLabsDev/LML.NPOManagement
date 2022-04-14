

namespace LML.NPOManagement.Bll.Model
{
    public class UserInformationModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserTypeEnum UserTypeEnum { get; set; }
        public string FirstName { get; set; }
        public string Token { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string PhoneNumber { get; set; } 
        public Gender Gender { get; set; }
        public string Metadata { get; set; }
        public virtual UserModel User { get; set; } 
    }
}
