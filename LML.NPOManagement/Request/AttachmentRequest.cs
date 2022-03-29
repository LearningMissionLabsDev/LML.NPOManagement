using System.ComponentModel.DataAnnotations;

namespace LML.NPOManagement.Request
{
    public class AttachmentRequest
    {
        [Required]
        [DataType(DataType.DateTime)]
        public byte[] AttachmentData { get; set; }
    }
}
