using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Common.Model
{
    public class SearchModel
    {
        public int Id { get; set; }
        public SearchTypeEnum Type { get; set; }
        public String Name { get; set; }
    }
}
