using System.Security.Cryptography;
using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LML.NPOManagement.Bll.Services
{
    public class KeyService : IKeyService
	{
        private IMapper _mapper;
        private readonly INPOManagementContext _dbContext;
        private readonly IConfiguration _configuration;

        public KeyService(INPOManagementContext context, IConfiguration configuration)
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


        public async Task<KeyModel> GetKey(string recovery)
        {
            var key = await _dbContext.Keys.Where(k => k.Recovery == recovery).FirstOrDefaultAsync();
            if (key != null)
            {
                return _mapper.Map<Key, KeyModel>(key);
            }

            return null;
        }

        public async Task<KeyModel> GenerateKeys(string email) 
        {
            string publicKey;
            string privateKey;
            using (RSACryptoServiceProvider rsa = new())
            {
                publicKey = rsa.ToXmlString(false);
                privateKey = rsa.ToXmlString(true);
            }
            
            var key = new Key()
            {
                PublicKey = publicKey,
                PrivateKey = privateKey,
                Recovery = email
            };

            await _dbContext.Keys.AddAsync(key);
            await _dbContext.SaveChangesAsync();

            var newKey = await _dbContext.Keys.Where(k => k.Recovery == email).FirstOrDefaultAsync();
            if (newKey == null)
            {
                return null;
            }

            var newKeyModel = _mapper.Map<Key, KeyModel>(newKey);
            return newKeyModel;
        }
    }
}

