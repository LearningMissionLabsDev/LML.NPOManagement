using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class Appointment
{
    public int Id { get; set; }

    public int CreatorId { get; set; }

    public int AccountId { get; set; }

    public string Title { get; set; } = null!;

    public int RepeatTime { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Description { get; set; }
}
