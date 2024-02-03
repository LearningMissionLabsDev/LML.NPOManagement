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
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? StatusId { get; set; }
        public virtual ICollection<AccountProgressModel> AccountProgresses { get; } = new List<AccountProgressModel>();
        public virtual AccountStatusModel? Status { get; set; }
        public virtual ICollection<UserModel> Users { get; } = new List<UserModel>();
    }
}
