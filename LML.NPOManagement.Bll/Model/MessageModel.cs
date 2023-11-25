using System;
namespace LML.NPOManagement.Bll.Model
{
	public class MessageModel
	{
        public int ID { get; set; }
        public string Sender { get; set; } = null!;
        public string Recovery { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool FromUser { get; set; } 
    }
}
