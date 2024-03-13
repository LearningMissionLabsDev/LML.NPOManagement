using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Common.Model
{
    public class AccountModel
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int CreatorId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? Description { get; set; }
        public string Token { get; set; }
        public virtual ICollection<Account2UserModel> Account2Users { get; } = new List<Account2UserModel>();
        public virtual UserModel Creator { get; set; } = null!;
        public virtual AccountStatusModel Status { get; set; } = null!;
    }
}
