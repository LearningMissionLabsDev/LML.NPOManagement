using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class UserTypeResponse
    {
        public UserTypeResponse()
        {
            Users = new HashSet<UserResponse>();
        }

        public int Id { get; set; }
        public string Description { get; set; } 

        public virtual ICollection<UserResponse> Users { get; set; }
    }
}
