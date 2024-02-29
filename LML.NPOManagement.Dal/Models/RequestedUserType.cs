using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class RequestedUserType
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<UserInformation> UserInformations { get; } = new List<UserInformation>();
}
