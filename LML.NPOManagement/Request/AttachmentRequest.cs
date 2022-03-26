namespace LML.NPOManagement.Request
{
    public class AttachmentRequest
    {
        public int NotificationId { get; set; }
        public byte[] AttachmentData { get; set; }
    }
}
