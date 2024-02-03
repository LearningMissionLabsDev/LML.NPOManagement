using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class UsersGroupRequest
    {
        [Required]
        public string GroupName { get; set; } = null!;
        public string? Description { get; set; }
        public List<int> UserIds { get; set; }
    }
}
