using LML.NPOManagement.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Models
{
    public class DonationResponse
    {
        public DonationResponse(Donation donation)
        {
            Id = donation.Id;
            Amount = donation.Amount;   
            DateOfCharity = donation.DateOfCharity; 
        }
        public int Id { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DateOfCharity { get; set; }

    }
}
