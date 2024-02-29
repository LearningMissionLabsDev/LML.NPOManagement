using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Common.Model;

public class RequestedUserTypeModel
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public virtual ICollection<UserInformationModel> UserInformations { get; } = new List<UserInformationModel>();
}
