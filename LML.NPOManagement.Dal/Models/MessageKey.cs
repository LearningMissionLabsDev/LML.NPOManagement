namespace LML.NPOManagement.Dal.Models
{
    public class MessageKey
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public string PrivateKey { get; set; } = null!;
    }
}
