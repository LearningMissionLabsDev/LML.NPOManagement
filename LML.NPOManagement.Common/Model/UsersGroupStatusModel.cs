using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Common.Model
{
    public class UsersGroupStatusModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public virtual ICollection<UsersGroupModel> UsersGroups { get; } = new List<UsersGroupModel>();
    }
}
