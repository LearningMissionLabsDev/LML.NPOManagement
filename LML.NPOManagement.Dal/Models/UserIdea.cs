using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class UserIdea
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? IdeaCategory { get; set; }
    public string IdeaDeskcription { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
