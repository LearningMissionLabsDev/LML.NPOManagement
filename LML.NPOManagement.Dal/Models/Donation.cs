using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class Donation
{
    public int Id { get; set; }

    public int InvestorId { get; set; }

    public decimal Amount { get; set; }

    public DateTime DateOfCharity { get; set; }

    public virtual InvestorInformation Investor { get; set; } = null!;
}
