using System;
namespace LML.NPOManagement.Bll.Model
{
	public class KeyModel
	{
        public int Id { get; set; }
        public string Recovery { get; set; } = null!;
        public string PublicKey { get; set; } = null!;
        public string PrivateKey { get; set; } = null!;

    }
}

