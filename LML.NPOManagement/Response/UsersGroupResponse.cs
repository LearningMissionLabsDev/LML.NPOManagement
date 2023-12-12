using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Response
{
    public class UsersGroupResponse
    {
        public int Id { get; set; }
        public int CreatedByUserId { get; set; }
        public string GroupName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string? Description { get; set; }
    }
}
