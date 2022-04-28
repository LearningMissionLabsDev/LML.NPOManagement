using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Response
{
    public class UserInformationResponse
    {
        public UserTypeEnum UserTypeEnum { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
