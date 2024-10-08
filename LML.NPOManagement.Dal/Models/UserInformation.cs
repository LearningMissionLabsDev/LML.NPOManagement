﻿using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class UserInformation
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RequestedUserRoleId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public int Gender { get; set; }

    public string? UserImage { get; set; }

    public string? Metadata { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual RequestedUserType RequestedUserRole { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
