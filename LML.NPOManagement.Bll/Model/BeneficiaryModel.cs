using LML.NPOManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Model
{
    public class BeneficiaryModel
    {
        public BeneficiaryModel()
        {
            AccountManagerInfos = new HashSet<AccountManagerInfo>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public int AccountManagerCategoryId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string BeneficiaryInfo { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;

        public virtual ICollection<AccountManagerInfo> AccountManagerInfos { get; set; }
    }
}
