using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Bll.Model
{
    public partial class UserIdeaModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? IdeaCategory { get; set; }
        public string IdeaDeskcription { get; set; } 

        public virtual UserModel User { get; set; } 
    }
}
