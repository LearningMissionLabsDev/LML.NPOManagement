namespace LML.NPOManagement.Dal.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string Subject { get; set; } = null!;
        public string message { get; set; } = null!;
        public bool Secret { get; set; } = false;
        public bool Opened { get; set; } = false;
    }
}