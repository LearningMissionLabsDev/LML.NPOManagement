using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BC = BCrypt.Net.BCrypt;

namespace LML.NPOManagement.Bll.Services
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        private readonly INPOManagementContext _dbContext;       
        public UserService(INPOManagementContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountProgress, AccountProgressModel>();
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<Attachment, AttachmentModel>();
                cfg.CreateMap<DailySchedule, DailyScheduleModel>();
                cfg.CreateMap<Donation, DonationModel>();
                cfg.CreateMap<Account, AccountModel>();
                cfg.CreateMap<InvestorInformation, InvestorInformationModel>();
                cfg.CreateMap<InventoryType, InventoryTypeModel>();
                cfg.CreateMap<MeetingSchedule, MeetingScheduleModel>();
                cfg.CreateMap<Notification, NotificationModel>();
                cfg.CreateMap<Template, TemplateModel>();
                cfg.CreateMap<TemplateType, TemplateTypeModel>();
                cfg.CreateMap<UserInformation, UserInformationModel>();
                cfg.CreateMap<UserInventory, UserInventoryModel>();
                cfg.CreateMap<UserType, UserTypeModel>();
                cfg.CreateMap<AccountProgressModel, AccountProgress>();
                cfg.CreateMap<AttachmentModel, Attachment>();
                cfg.CreateMap<DailyScheduleModel, DailySchedule>();
                cfg.CreateMap<DonationModel, Donation>();
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<InvestorInformationModel, InvestorInformation>();
                cfg.CreateMap<InventoryTypeModel, InventoryType>();
                cfg.CreateMap<MeetingScheduleModel, MeetingSchedule>();
                cfg.CreateMap<NotificationModel, Notification>();
                cfg.CreateMap<TemplateModel, Template>();
                cfg.CreateMap<TemplateTypeModel, TemplateType>();
                cfg.CreateMap<UserInformationModel, UserInformation>();
                cfg.CreateMap<UserInventoryModel, UserInventory>();
                cfg.CreateMap<UserTypeModel, UserType>();
                cfg.CreateMap<UserModel, User>();
            });
            _mapper = config.CreateMapper();
            _dbContext = context;
        }

        public void DeleteUser(int id)
        {
            //using(var dbContext = new NPOManagementContext())
            //{
                var user = _dbContext.Users.Where(us => us.Id == id).FirstOrDefault();
                user.Status = Convert.ToString(StatusEnumModel.Closed);
                _dbContext.SaveChangesAsync();
            //}
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            //using (var dbContext = new NPOManagementContext())
            //{
            List<UserModel> userModels = new List<UserModel>();
            var users = await _dbContext.Users.ToListAsync();
            foreach (var user in users)
            {
                var userModel = _mapper.Map<User, UserModel>(user);
                userModels.Add(userModel);
            }
            return userModels;
            //}
        }

        public async Task<List<UserTypeModel>>  GetAllUserTypes()
        {
            //using(var dbContext = new NPOManagementContext())
            //{
            List<UserTypeModel> userTypeModels = new List<UserTypeModel>();
            var types = await _dbContext.UserTypes.ToListAsync();
            foreach (var type in types)
            {
                var userType = _mapper.Map<UserType, UserTypeModel>(type);
                userTypeModels.Add(userType);
            }
            return userTypeModels;
            //}
        }

        public async Task<UserModel> GetUserById(int id)
        {
            //using(var dbContext = new NPOManagementContext())
            //{
                var user = await _dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (user != null)
                {
                    var userModel = _mapper.Map<User,UserModel>(user);
                    return userModel;
                }
                return null;
            //}
        }

        public async Task<UserModel> Login(UserModel userModel, IConfiguration configuration)
        {
            //using (var dbContext = new NPOManagementContext())
            //{
                var user = await _dbContext.Users.FirstOrDefaultAsync(m => m.Email == userModel.Email);
                if (user != null && BC.Verify(userModel.Password, user.Password))
                {
                    var userModelMapper = _mapper.Map<User, UserModel>(user);
                    userModelMapper.Password = null;
                    userModelMapper.Token = TokenCreationHelper.GenerateJwtToken(userModelMapper, configuration);
                    return userModelMapper;
                }
            //}
            return null;
        }

        public async Task<bool> ModifyUser(UserModel userModel, int id)
        {
            //using(var dbContext = new NPOManagementContext())
            //{
                var user =await _dbContext.Users.Where(us => us.Id == id).FirstOrDefaultAsync();
                var verifyUser = BC.Verify(userModel.Password,user.Password);
                if (verifyUser)
                {
                    var modifyUser = _mapper.Map<UserModel, User>(userModel);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            //}
        }

        public async Task<UserModel> Registration(UserModel userModel, IConfiguration configuration)
        {
            //using (var dbContext = new NPOManagementContext())
            //{              
                var user = await _dbContext.Users.FirstOrDefaultAsync(m => m.Email == userModel.Email);
       
                if (user == null )
                {             
                    userModel.Password = BC.HashPassword(userModel.Password);
                    var addUser = _mapper.Map<UserModel, User>(userModel);
                    _dbContext.Users.Add(addUser);
                    await _dbContext.SaveChangesAsync();
                    var newUser = _mapper.Map<User, UserModel>(addUser);
                    newUser.Token = TokenCreationHelper.GenerateJwtToken(newUser, configuration);
                    newUser.Password = null;
                    return newUser;                    
                }
                else if(user.Status == Convert.ToString(StatusEnumModel.Closed))
                {
                    return null;//to do hendle this condition differently
                }
                return null;
            //}           
        }

        public async Task<int> UserInformationRegistration(UserInformationModel userInformationModel,IConfiguration configuration )
        {
            //using(var dbContext = new NPOManagementContext())
            //{
                var userInfo = new UserInformation()
                {
                    FirstName = userInformationModel.FirstName,
                    LastName = userInformationModel.LastName,
                    DateOfBirth = userInformationModel.DateOfBirth,
                    CreateDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow,
                    UserId = userInformationModel.UserId,
                    Gender = (int)userInformationModel.Gender,
                    MiddleName = userInformationModel.MiddleName,
                    Metadata = userInformationModel.Metadata,
                    PhoneNumber = userInformationModel.PhoneNumber,
                };
                _dbContext.UserInformations.Add(userInfo);
                _dbContext.SaveChanges();
                if (userInformationModel.UserTypeEnum == UserTypeEnum.Investor)
                {
                    _dbContext.InvestorInformations.Add(new InvestorInformation()
                    {
                        UserId = userInformationModel.UserId,
                        InvestorTierId = Convert.ToInt16(InvestorTierEnum.Basic),
                    });
                   await _dbContext.SaveChangesAsync();
                }
               
                return userInfo.Id;
            //}
        }

        public async Task<List<UserModel>> GetUsersByRole(int id)
        {
            //using (var dbContext = new NPOManagementContext())
            //{
                var userRole = await _dbContext.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();
                var users = await _dbContext.Users.Where(ro => ro.Roles.Contains(userRole)).ToListAsync();
                if (users.Count > 0)
                {
                    var userModel = new List<UserModel>();
                    foreach (var user in users)
                    {
                        var userByRole = _mapper.Map<User, UserModel>(user);                     
                        userModel.Add(userByRole);
                    }
                    return userModel;
                }
                return null;
            //}
        }

        public async Task<List<UserModel>> GetUsersByAccount(int id)
        {
            //using (var dbContext = new NPOManagementContext())
            //{
                var account = await _dbContext.Accounts.Where(acc => acc.Id == id).FirstOrDefaultAsync();
                var users = await _dbContext.Users.Where(acc => acc.Accounts.Contains(account)).ToListAsync();
                if (users.Count > 0)
                {
                    var userModel = new List<UserModel>();
                    foreach (var user in users)
                    {
                        var userByAccount = _mapper.Map<User, UserModel>(user);
                        userModel.Add(userByAccount);
                    }
                    return userModel;
                }
                return null;
            //}
        }

        public async Task<List<UserModel>> GetUsersByInvestorTier(int id)
        {
            //using (var dbContext = new NPOManagementContext())
            //{
                var investor = await _dbContext.InvestorInformations.Where(inv => inv.InvestorTierId == id).FirstOrDefaultAsync();

                var users = await _dbContext.Users.Where(inv => inv.Id == investor.UserId).ToListAsync();

                if (users.Count > 0)
                {
                    var userModel = new List<UserModel>();
                    foreach (var user in users)
                    {
                        var userByAccount = _mapper.Map<User, UserModel>(user);
                        userModel.Add(userByAccount);
                    }
                    return userModel;
                }
                return null;
            //}
        }

        public async void AddUserType(UserInformationModel userInformationModel)
        {
            //using(var dbContext = new NPOManagementContext())
            //{
                var userTypes = _dbContext.UserTypes.ToList();
                var user = _dbContext.Users.Where(us => us.Id == userInformationModel.UserId).FirstOrDefault();
                foreach (var userType in userTypes)
                {
                    if( userType.Description == Convert.ToString( userInformationModel.UserTypeEnum ))
                    {
                        user.UserTypes.Add(userType);
                       await _dbContext.SaveChangesAsync();
                    }
                }
            //}
        }

        public async Task<UserModel> ActivationUser(string token,IConfiguration configuration)
        {
            //using (var dbContext = new NPOManagementContext())
            //{
                var newUser = TokenCreationHelper.ValidateJwtToken(token, configuration);
                var user = await _dbContext.Users.Where(us => us.Id == newUser.Id).FirstOrDefaultAsync();
                user.Status = StatusEnumModel.Activ.ToString();
                await _dbContext.SaveChangesAsync();
                var userModel = _mapper.Map<User,UserModel>(user);
                return userModel;
            //}

        }
      
    }
}
