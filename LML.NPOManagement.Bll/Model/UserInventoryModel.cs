﻿using LML.NPOManagement.Dal.Models;

namespace LML.NPOManagement.Bll.Model
{
    public class UserInventoryModel
    {
        public int Id { get; set; }
        public int InventoryTypeId { get; set; }
        public int? UserId { get; set; }
        public DateTime Date { get; set; }
        public string Metadata { get; set; } = null!;
        public string? Description { get; set; }

        public virtual InventoryTypeModel InventoryType { get; set; } = null!;
        public virtual UserModel? User { get; set; }
    }
}
