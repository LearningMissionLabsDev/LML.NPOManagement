namespace LML.NPOManagement.Bll.Model
{
    public class MessageModel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string SenderEmail { get; set; } = null!;
        public string RecipientEmail { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool Secret { get; set; } = false;
        public bool Opened { get; set; } = false;
    }
}