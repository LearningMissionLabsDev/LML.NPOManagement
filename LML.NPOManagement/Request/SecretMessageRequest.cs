namespace LML.NPOManagement.Request
{
    public class SecretMessageRequest
    {
        public int messageId { get; set; }
        public string password { get; set; }
        public string publicKey { get; set; }
    }
}
