using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class Key
    {
        public int Id { get; set; }
        public string Recovery { get; set; } = null!;
        public string PublicKey { get; set; } = null!;
        public string PrivateKey { get; set; } = null!;
    }
}
