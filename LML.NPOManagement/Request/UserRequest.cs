using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Request
{
    public class UserRequest
    {
        public int UserTypeId { get; set; }
        public int UserInformationId { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string Status { get; set; }

    }
}
