using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Model
{
    public class InvestorTierTypeModel
    {
        public InvestorTierTypeModel()
        {
            InvestorInformations = new HashSet<InvestorInformationModel>();
        }

        public int Id { get; set; }
        public InvestorTier investorTier { get; set; } 

        public virtual ICollection<InvestorInformationModel> InvestorInformations { get; set; }
    }
}
