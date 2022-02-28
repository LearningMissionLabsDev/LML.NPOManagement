﻿using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Bll.Model
{
    public class UserInformationModel
    {
        public UserInformationModel()
        {
            Users = new HashSet<UserModel>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string PhoneNumber { get; set; } 
        public Gender gender{ get; set; }
        public string UserInfo { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }
    }
}