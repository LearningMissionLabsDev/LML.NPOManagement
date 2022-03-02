using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Request 
{ 
    public class UserRegistrationRequest
    {
        public UserRegistrationRequest(UserModel userModel, UserInformationModel userInformationModel, UserTypeModel userTypeModel)
        {
            userModel.Password = Password;
            userModel.Email = Email;
            userInformationModel.FirstName = FirstName;
            userInformationModel.LastName = LastName;   
            userInformationModel.MiddleName = MiddleName;
            userInformationModel.DateOfBirth = DateOfBirth;
            userInformationModel.CreateDate = CreateDate;
            userInformationModel.UpdateDate = UpdateDate;
            userInformationModel.PhoneNumber = PhoneNumber;
            userInformationModel.Gender = (int) GenderId;
            userInformationModel.UserInfo = UserInfo;
            userTypeModel.Description = Convert.ToString(UserTypeId);
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string PhoneNumber { get; set; } 
        public Gender GenderId{ get; set; }
        public string UserInfo { get; set; }

    }
}
