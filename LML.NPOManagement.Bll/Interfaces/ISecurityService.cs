using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;


namespace LML.NPOManagement.Bll.Interfaces
{
    public interface ISecurityService
    {
        public Task<string> CreateNewKey(ConnectionInformation connectInfo);
        public Task<string> Encrypt(string message, string key);
        public Task<string> Decrypt(string message, string key);
        public Task<bool> AddSecretMessage(Message message, ConnectionInformation connectInfo);
        public Task<bool> VerifyPassword(string encryptedPassword, string hashedPassword, ConnectionInformation connectInfo);
    }
}
