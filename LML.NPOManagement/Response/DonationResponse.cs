﻿namespace LML.NPOManagement.Response
{
    public class DonationResponse
    {
        public int Id { get; set; }
        public int InvestorId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfCharity { get; set; }
    }
}
