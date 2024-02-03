using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models;

public partial class InventoryType
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public virtual ICollection<UserInventory> UserInventories { get; } = new List<UserInventory>();
}
