using LML.NPOManagement.Bll.Model;
using Microsoft.AspNetCore.Http;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IMessageService
    {
        public Task<List<MessageModel>> GetMyMessages(int userId);
        public Task<MessageModel> GetMessageById(int messageId, int userId);
        public Task<MessageModel> GetSecretMessage(int userId, int messageId, string password, string publicKey, ConnectionInformation connectInfo);
         public Task<bool> AddMessage(MessageModel messageModel, ConnectionInformation connectInfo);
        public Task<int> NewMessagesCount(int userId);
    }
}