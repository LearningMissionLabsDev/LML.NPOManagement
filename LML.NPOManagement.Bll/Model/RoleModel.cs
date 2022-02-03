using LML.NPOManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Model
{
    public class RoleModel
    {
        public RoleModel()
        {
            AccountManagerInfos = new HashSet<AccountManagerInfo>();
            Beneficiaries = new HashSet<Beneficiary>();
        }

        public int Id { get; set; }
        public string Role1 { get; set; } = null!;

        public virtual ICollection<AccountManagerInfo> AccountManagerInfos { get; set; }
        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }
    }
}
