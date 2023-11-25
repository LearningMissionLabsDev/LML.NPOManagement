using System;
using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Bll.Utilities;
using LML.NPOManagement.Dal;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LML.NPOManagement.Bll.Services
{
	public class MessageService : IMessageService
    {
        private IMapper _mapper;
        private readonly INPOManagementContext _dbContext;
        private readonly IConfiguration _configuration;
       
        public MessageService(INPOManagementContext context, IConfiguration configuration)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountProgress, AccountProgressModel>();
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<Attachment, AttachmentModel>();
                cfg.CreateMap<Donation, DonationModel>();
                cfg.CreateMap<Account, AccountModel>();
                cfg.CreateMap<InvestorInformation, InvestorInformationModel>();
                cfg.CreateMap<InventoryType, InventoryTypeModel>();
                cfg.CreateMap<Notification, NotificationModel>();
                cfg.CreateMap<UserInformation, UserInformationModel>();
                cfg.CreateMap<UserInventory, UserInventoryModel>();
                cfg.CreateMap<UserType, UserTypeModel>();
                cfg.CreateMap<AccountProgressModel, AccountProgress>();
                cfg.CreateMap<AttachmentModel, Attachment>();
                cfg.CreateMap<DonationModel, Donation>();
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformation>();
                cfg.CreateMap<InventoryTypeModel, InventoryType>();
                cfg.CreateMap<NotificationModel, Notification>();
                cfg.CreateMap<UserInformationModel, UserInformation>();
                cfg.CreateMap<UserInventoryModel, UserInventory>();
                cfg.CreateMap<UserTypeModel, UserType>();
                cfg.CreateMap<UserModel, User>();
                cfg.CreateMap<MessageModel, Messaging>();
                cfg.CreateMap<Messaging, MessageModel>();
                cfg.CreateMap<Key, KeyModel>();
                cfg.CreateMap<KeyModel, Key>();
            });
            _mapper = config.CreateMapper();
            _dbContext = context;
            _configuration = configuration;
        }

        public async Task<MessageModel> AddMessage(MessageModel messageModel)
        {
            var messaging = _mapper.Map<MessageModel, Messaging>(messageModel);
            var keys = await _dbContext.Keys.Where(key => key.Recovery == messageModel.Recovery).FirstOrDefaultAsync();
            if (keys == null)
            {
                return null;
            }
            string publicKey = keys.PublicKey;
            messaging.Message = RSA.Encrypt(publicKey,messaging.Message);
            await _dbContext.Messagings.AddAsync(messaging);
            await _dbContext.SaveChangesAsync();
            var newMessaging = await _dbContext.Messagings.Where(m => m.Sender == messageModel.Sender
                                                                    && m.Recovery == messageModel.Recovery).FirstOrDefaultAsync();
            if (newMessaging == null)
            {
                return null;
            }

            var newModel = _mapper.Map<Messaging, MessageModel>(newMessaging); 
            return newModel;
        }

        public async Task<List<MessageModel>> GetMessages(string currentUserEmail, string currentSobes)
        {
            var messageModels = new List<MessageModel>();
            var messages = await _dbContext.Messagings.ToListAsync();

            foreach (var message in messages)
            {
                if (message.Sender == currentUserEmail && message.Recovery == currentSobes)
                {
                    var keys = await _dbContext.Keys.Where(key => key.Recovery == currentSobes).FirstOrDefaultAsync();
                    var privateKey = keys.PrivateKey;
                    message.Message = RSA.Decrypt(privateKey, message.Message);
                    var messageModel = _mapper.Map<Messaging, MessageModel>(message);
                    messageModel.FromUser = true;
                    messageModels.Add(messageModel);
                }
                else if (message.Recovery == currentUserEmail && message.Sender == currentSobes)
                {
                    var keys = await _dbContext.Keys.Where(key => key.Recovery == currentUserEmail).FirstOrDefaultAsync();
                    var privateKey = keys.PrivateKey;
                    message.Message = RSA.Decrypt(privateKey, message.Message);
                    var messageModel = _mapper.Map<Messaging, MessageModel>(message);
                    messageModel.FromUser = false;
                    messageModels.Add(messageModel);
                }
            }

            return messageModels;
        }
    }
}

