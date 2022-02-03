using LML.NPOManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Model
{
    public class AccountManagerModel
    {
        public AccountManagerModel()
        {
            AccountManagerInfos = new HashSet<AccountManagerInfo>();
        }

        public int Id { get; set; }
        public string AccountManagerCategory { get; set; } = null!;
        public string? NarrowProfessional { get; set; }

        public virtual ICollection<AccountManagerInfo> AccountManagerInfos { get; set; }
    }
}
