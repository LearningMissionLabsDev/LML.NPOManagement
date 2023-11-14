namespace LML.NPOManagement.Response
{
    public class MessageResponse
    {
        public int Id { get; set; }
        public string SenderEmail { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool Secret { get; set; }
        public bool Opened { get; set; }
    }
}
