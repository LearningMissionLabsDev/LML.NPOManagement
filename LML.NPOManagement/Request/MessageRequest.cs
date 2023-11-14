namespace LML.NPOManagement.Request
{
    public class MessageRequest
    {
        public string RecipientEmail { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool Secret { get; set; } = false;
    }
}