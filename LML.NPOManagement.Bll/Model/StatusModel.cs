using LML.NPOManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Model
{
    public class StatusModel
    {
        public StatusModel()
        {
            AccountManagerInfos = new HashSet<AccountManagerInfo>();
            Beneficiaries = new HashSet<Beneficiary>();
        }

        public int Id { get; set; }
        public string StatusType { get; set; } = null!;

        public virtual ICollection<AccountManagerInfo> AccountManagerInfos { get; set; }
        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }
    }
}
