using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Request
{
    public class UsersGroupRequest
    {
        public string GroupName { get; set; } = null!;
        public int CreatedByUserId { get; set; }
        public string? Description { get; set; }
    }
}
