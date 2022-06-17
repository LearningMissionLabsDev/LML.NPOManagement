using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Bll.Services
{
    public class UserInventoryService : IUserInventoryService
    {
        private IMapper _mapper;
        private readonly INPOManagementContext _dbContext;
        public UserInventoryService(INPOManagementContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountProgress, AccountProgressModel>();
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
            });
            _mapper = config.CreateMapper();
            _dbContext = context;
        }

        public async Task<UserInventoryModel> AddUserInventory(UserInventoryModel userInventoryModel)
        {
            var inventory = _mapper.Map<UserInventoryModel,UserInventory>(userInventoryModel);
            await _dbContext.UserInventories.AddAsync(inventory);
            await _dbContext.SaveChangesAsync();
            var newInventory = await _dbContext.UserInventories.Where(inv => inv.Date == userInventoryModel.Date).FirstOrDefaultAsync();
            if (newInventory == null)
            {
                return null;
            }
            var inventoryModel = _mapper.Map<UserInventory,UserInventoryModel>(newInventory);
            return inventoryModel;
        }

        public async Task<List<UserInventoryModel>> GetAllUserInventories()
        {
            var inventories = await _dbContext.UserInventories.ToListAsync();
            if(inventories.Count == 0)
            {
                return null;
            }
            return _mapper.Map<List<UserInventory>, List<UserInventoryModel>>(inventories);
        }

        public async Task<UserInventoryModel> GetUserInventoryById(int id)
        {
            var inventory = await _dbContext.UserInventories.Where(inv => inv.Id == id).FirstOrDefaultAsync();
            if(inventory == null)
            {
                return null;
            }
            return _mapper.Map<UserInventory, UserInventoryModel>(inventory);
        }

        public async Task<UserInventoryModel> ModifyUserInventory(UserInventoryModel userInventoryModel, int id)
        {
            var inventory = await _dbContext.UserInventories.Where(inv => inv.Id == id).FirstOrDefaultAsync();
            if( inventory == null)
            {
                return null;
            }
            inventory.Description = userInventoryModel.Description;
            inventory.InventoryTypeId = userInventoryModel.InventoryTypeId;
            inventory.Metadata = userInventoryModel.Metadata;
            inventory.Date = userInventoryModel.Date;
            inventory.UserId = userInventoryModel.UserId;
            await _dbContext.UserInventories.AddAsync(inventory);
            await _dbContext.SaveChangesAsync();
            var inventoryModel = _mapper.Map<UserInventory,UserInventoryModel>(inventory);
            return inventoryModel;

        }

        public async Task<InventoryTypeModel> GetUserInventoryTypeById(int id)
        {
            var inventoryType = await _dbContext.InventoryTypes.Where(type => type.Id == id).FirstOrDefaultAsync();
            if(inventoryType == null)
            {
                return null;
            }
            return _mapper.Map<InventoryType, InventoryTypeModel>(inventoryType);
        }

        public async Task<InventoryTypeModel> AddInventoryType(InventoryTypeModel inventoryTypeModel)
        {
            var inventory = _mapper.Map<InventoryTypeModel, InventoryType>(inventoryTypeModel);
            await _dbContext.InventoryTypes.AddAsync(inventory);
            await _dbContext.SaveChangesAsync();
            return inventoryTypeModel;
        }

        public async Task<string> GetAllInventoryTypes(string type, DateTime dateTimeStart, DateTime dateTimeFinsh)
        {
            List<UserInventoryModel> inventoryModels = new List<UserInventoryModel>();
            var amount = string.Empty;

            if (((dateTimeStart <= DateTime.UtcNow) && (dateTimeStart.Year >= 2020)) && (( dateTimeFinsh <= DateTime.UtcNow) && (dateTimeFinsh.Year >= 2020)))
            {
                var inventories = await _dbContext.UserInventories.Where(inv => inv.InventoryType.Description == type &&
                (inv.Date <= dateTimeFinsh && inv.Date >= dateTimeStart)).ToListAsync();
                foreach (var inventory in inventories)
                {
                    amount += inventory.Amount;
                    var model = _mapper.Map<UserInventory, UserInventoryModel>(inventory);
                    inventoryModels.Add(model);
                }
            }
            
            
            if(inventoryModels.Count == 0)
            {
                return null;
            }


            return amount;
        }

        public async Task<List<UserInventoryModel>> GetInventoryByUser(int id)
        {
            var userInventories = await _dbContext.UserInventories.Where(inv => inv.UserId == id).ToListAsync();
            if(userInventories.Count == 0)
            {
                return null;
            }
            List<UserInventoryModel> inventoryModels = new List<UserInventoryModel>();
            foreach(var userInventory in userInventories)
            {
                var newInventoryModel = _mapper.Map<UserInventory,UserInventoryModel>(userInventory);
                inventoryModels.Add(newInventoryModel);
            }
            return inventoryModels;
        }

        public async Task<List<UserInventoryModel>> GetInventoryUserByYear(DateTime dateTimeStart, DateTime dateTimeFinish, int id)
        {
            if ((dateTimeStart >= DateTime.UtcNow || dateTimeFinish >= DateTime.UtcNow) && dateTimeStart >= dateTimeFinish)
            {
                return null;
            }
            var inventories = await _dbContext.UserInventories.Where(inv => inv.UserId == id && (( inv.Date == dateTimeStart) &&
            (inv.Date == dateTimeFinish))).ToListAsync();

            if(inventories.Count == 0)
            {
                return null;
            }
            List<UserInventoryModel> userInventoryModels = new List<UserInventoryModel>();
            foreach (var inventory in inventories)
            {
                var inventoryModel = _mapper.Map<UserInventory, UserInventoryModel>(inventory);
                userInventoryModels.Add(inventoryModel);
            }
            return userInventoryModels;
        }

        public async Task<List<UserInventoryModel>> GetInventoryByYear(DateTime dateTimeStart, DateTime dateTimeFinish)
        {
            if ((dateTimeStart >= DateTime.UtcNow || dateTimeFinish >= DateTime.UtcNow) && dateTimeStart >= dateTimeFinish)
            {
                return null;
            }
            var inventories = await _dbContext.UserInventories.Where(inv => (inv.Date == dateTimeStart) &&
            (inv.Date == dateTimeFinish)).ToListAsync();

            if (inventories.Count == 0)
            {
                return null;
            }
            List<UserInventoryModel> userInventoryModels = new List<UserInventoryModel>();
            foreach (var inventory in inventories)
            {
                var inventoryModel = _mapper.Map<UserInventory, UserInventoryModel>(inventory);
                userInventoryModels.Add(inventoryModel);
            }
            return userInventoryModels;
        }

        public async Task<UserInventoryModel> DeleteInventory(int id)
        {
            var inventory = await _dbContext.UserInventories.Where(inv => inv.Id == id).FirstOrDefaultAsync();
            if(inventory == null)
            {
                return null;
            }
            inventory.Status = (int)StatusEnumModel.Closed;
            await _dbContext.SaveChangesAsync();
            var model =_mapper.Map<UserInventory,UserInventoryModel>(inventory);
            return model;
        }
    }
}
