using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class Investor
    {
        public Investor()
        {
            Donations = new HashSet<Donation>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Donation> Donations { get; set; }
    }
}
