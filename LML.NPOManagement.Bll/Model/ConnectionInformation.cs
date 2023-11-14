namespace LML.NPOManagement.Bll.Model
{
    public class ConnectionInformation
    {
        public string Id { get; set; } = null!;
        public string IP { get; set; } = null!;
        public int Port { get; set; }
        public string privateKey { get; set; } = null!;
        public string publicKey { get; set; } = null!;
    }
}
