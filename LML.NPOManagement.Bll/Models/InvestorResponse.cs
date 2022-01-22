using LML.NPOManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Models
{
    public class InvestorResponse
    {
        public InvestorResponse(Investor investor)
        {
            Id = investor.Id;
            Name = investor.Name;
            LastName = investor.LastName;
            PhoneNumber = investor.PhoneNumber;
            Email = investor.Email;
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int? PhoneNumber { get; set; }
        public string? Email { get; set; }

    }
}
