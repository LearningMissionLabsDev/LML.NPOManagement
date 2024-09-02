using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class Account
{
    public int Id { get; set; }

    public int StatusId { get; set; }

    /// <summary>
    /// If set to True, this account will be browsable by the newly registered user
    /// </summary>
    public bool? IsVisible { get; set; }

    /// <summary>
    /// This is optional field to set maximum numbers of members for this account. if zero or null then membership is unlimited.
    /// </summary>
    public int? MaxCapacity { get; set; }

    public int CreatorId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public string? OnboardingLink { get; set; }

    public string? AccountImage { get; set; }

    public string? Description { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Account2User> Account2Users { get; } = new List<Account2User>();

    public virtual User Creator { get; set; } = null!;

    public virtual AccountStatus Status { get; set; } = null!;
}
