using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Request
{
    public class DonationRequest
    {
        public int InvestorId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfCharity { get; set; }

    }
}
