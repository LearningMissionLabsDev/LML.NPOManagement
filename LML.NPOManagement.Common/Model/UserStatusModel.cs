﻿
namespace LML.NPOManagement.Common.Model
{
    public class UserStatusModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public virtual ICollection<UserModel> Users { get; } = new List<UserModel>();
    }
}
