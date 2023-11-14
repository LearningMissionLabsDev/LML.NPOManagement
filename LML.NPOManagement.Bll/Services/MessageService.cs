using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using BC = BCrypt.Net.BCrypt;

namespace LML.NPOManagement.Bll.Services
{
    public class MessageService : IMessageService
    {
        private IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INPOManagementContext _dbContext;
        private readonly ISecurityService _securityService;

        public MessageService(IHttpContextAccessor httpContextAccessor, INPOManagementContext context, ISecurityService securityService)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MessageModel, Message>();
                cfg.CreateMap<Message, MessageModel>();
            });
            _mapper = config.CreateMapper();

            _dbContext = context;
            _httpContextAccessor = httpContextAccessor;
            _securityService = securityService;
        }

        public async Task<List<MessageModel>> GetMyMessages(int userId)
        {
            var messagesList = await _dbContext.Messages.Where(message => message.RecipientId == userId).ToListAsync();
            List<MessageModel> messageModels = new List<MessageModel>();
            foreach (var message in messagesList)
            {
                var model = _mapper.Map<Message, MessageModel>(message);
                User sender = await _dbContext.Users.Where(user => user.Id == model.SenderId).FirstOrDefaultAsync();
                model.SenderEmail = sender.Email;
                messageModels.Add(model);
            }
            return messageModels;
        }

        public async Task<MessageModel> GetMessageById(int userId, int messageId)
        {
            var message = await _dbContext.Messages.Where(message => message.Id == messageId).FirstOrDefaultAsync();
            if (message == null || (message.RecipientId != userId && message.SenderId != userId))
            {
                return null;
            }
            message.Opened = true;
            User sender = await _dbContext.Users.Where(user => user.Id == message.SenderId).FirstOrDefaultAsync();
            var messageModel = _mapper.Map<Message, MessageModel>(message);
            messageModel.SenderEmail = sender.Email;
            await _dbContext.SaveChangesAsync();
            return messageModel;
        }

        public async Task<MessageModel> GetSecretMessage(int userId, int messageId, string encryptedPassword, string publicKey, ConnectionInformation connectInfo)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
            bool isVerified = await _securityService.VerifyPassword(encryptedPassword, user.Password, connectInfo);
            if (user != null && isVerified)
            {
                var messageModel = await GetMessageById(userId, messageId);
                var messageKeys = await _dbContext.MessagesKeys.Where(msg => msg.MessageId == messageModel.Id).FirstOrDefaultAsync();
                var decryptedMessage = await _securityService.Decrypt(messageModel.Message, messageKeys.PrivateKey);
                messageModel.Message = await _securityService.Encrypt(decryptedMessage, publicKey);
                return messageModel;
            }
            return null;
        }

        public async Task<bool> AddMessage(MessageModel messageModel, ConnectionInformation connectInfo)
        {
            User recipient = await _dbContext.Users.Where(user => user.Email == messageModel.RecipientEmail).FirstOrDefaultAsync();
            if (recipient == null)
            {
                return false;
            }
            messageModel.RecipientId = recipient.Id;
            var message = _mapper.Map<MessageModel, Message>(messageModel);
            if (message.Secret)
            {
                return await _securityService.AddSecretMessage(message, connectInfo);
            }
            _dbContext.Messages.Add(message);
            var saveResult = await _dbContext.SaveChangesAsync();
            return Convert.ToBoolean(saveResult);
        }

        public async Task<int> NewMessagesCount(int userId)
        {
            int count = _dbContext.Messages.Where(message => message.RecipientId == userId && message.Opened == false).Count();
            return count;
        }

    }
}
