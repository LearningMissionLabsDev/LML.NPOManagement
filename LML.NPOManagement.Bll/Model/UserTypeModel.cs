using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Bll.Model
{
    public class UserTypeModel
    {
        public UserTypeModel()
        {
            Users = new HashSet<UserModel>();
        }

        public int Id { get; set; }
        public string Description { get; set; } 

        public virtual ICollection<UserModel> Users { get; set; }
    }
}
