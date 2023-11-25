using System;
using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
	public class MessageRequest
	{
        [EmailAddress, Required]
        public string Sender { get; set; } = null!;
        [EmailAddress, Required]
        public string Recovery { get; set; } = null!;
        [Required]
        public string Message { get; set; } = null!;
    }
}

