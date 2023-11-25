using System;
using System.Collections.Generic;

namespace LML.NPOManagement.Dal.Models
{
    public partial class Messaging
    {
        public int Id { get; set; }
        public string Sender { get; set; } = null!;
        public string Recovery { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
