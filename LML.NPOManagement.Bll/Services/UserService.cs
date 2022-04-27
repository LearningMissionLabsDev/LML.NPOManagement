using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BC = BCrypt.Net.BCrypt;

namespace LML.NPOManagement.Bll.Services
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        public UserService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountProgress, AccountProgressModel>();
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
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<WeeklySchedule, WeeklyScheduleModel>();
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
                cfg.CreateMap<WeeklyScheduleModel, WeeklySchedule>();
            });
            _mapper = config.CreateMapper();
        }


        public void DeleteUser(int id)
        {
            using(var dbContext = new NPOManagementContext())
            {
                var user = dbContext.Users.Where(us => us.Id == id).FirstOrDefault();
                user.Status = Convert.ToString(StatusEnumModel.Closed);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            using (var dbContext = new NPOManagementContext())
            {
                var users = dbContext.Users.ToList();
                foreach (var user in users)
                {
                    var userModels = _mapper.Map<User, UserModel>(user);
                    yield return userModels;
                }
            }
        }

       
        public IEnumerable<UserTypeModel>  GetAllUserTypes()
        {
            using(var dbContext = new NPOManagementContext())
            {
                var types = dbContext.UserTypes.ToList();
                foreach (var type in types)
                {
                    var userType = _mapper.Map<UserType, UserTypeModel>(type);
                    yield return userType;
                }
            }
        }

        public UserModel GetUserById(int id)
        {
            using(var dbContext = new NPOManagementContext())
            {
                var user = dbContext.Users.Where(x => x.Id == id).FirstOrDefault();
                if (user != null)
                {
                    var userModel = _mapper.Map<User,UserModel>(user);
                    return userModel;
                }
                return null;
            }
        }

        public async Task<UserModel> Login(UserModel userModel, IConfiguration configuration)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(m => m.Email == userModel.Email);
                if (user != null && BC.Verify(userModel.Password, user.Password))
                {
                    var userModelMapper = _mapper.Map<User, UserModel>(user);
                    userModelMapper.Password = null;
                    userModelMapper.Token = TokenCreationHelper.GenerateJwtToken(userModelMapper, configuration);
                    return userModelMapper;
                }
            }
            return null;
        }

        public bool ModifyUser(UserModel userModel, int id)
        {
            using(var dbContext = new NPOManagementContext())
            {
                var user = dbContext.Users.Where(us => us.Id == id).FirstOrDefault();
                var verifyUser = BC.Verify(userModel.Password,user.Password);
                if (verifyUser)
                {
                    var modifyUser = _mapper.Map<UserModel, User>(userModel);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public async Task<UserModel> Registration(UserModel userModel, IConfiguration configuration)
        {
            using (var dbContext = new NPOManagementContext())
            {              
                var user = await dbContext.Users.FirstOrDefaultAsync(m => m.Email == userModel.Email);
       
                if (user == null )
                {                    
                    userModel.Password = BC.HashPassword(userModel.Password);
                    var addUser = _mapper.Map<UserModel, User>(userModel);
                    dbContext.Users.Add(addUser);
                    dbContext.SaveChanges();
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
            }           
        }
        public async Task<int> UserInformationRegistration(UserInformationModel userInformationModel,IConfiguration configuration )
        {
            using(var dbContext = new NPOManagementContext())
            {
                var userInfo = _mapper.Map<UserInformationModel, UserInformation>(userInformationModel);
                dbContext.UserInformations.Add(userInfo);
                dbContext.SaveChanges();
                if (userInformationModel.UserTypeEnum == Model.UserTypeEnum.Investor)
                {
                    dbContext.InvestorInformations.Add(new InvestorInformation()
                    {
                        UserId = userInformationModel.UserId,
                        InvestorTierId = Convert.ToInt16(InvestorTierEnum.Basic),
                    });
                    dbContext.SaveChanges();

                }
                
                return userInfo.Id;
            }
        }




        public async Task<List<UserModel>> GetUsersByRole(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var userRole = await dbContext.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();
                var users = await dbContext.Users.Where(ro => ro.Roles.Contains(userRole)).ToListAsync();
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
            }

        }

        public async Task<List<UserModel>> GetUsersByAccount(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var account = await dbContext.Accounts.Where(acc => acc.Id == id).FirstOrDefaultAsync();
                var users = await dbContext.Users.Where(acc => acc.Accounts.Contains(account)).ToListAsync();
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
            }
        }

        public async Task<List<UserModel>> GetUsersByInvestorTier(int id)
        {
            using (var dbContext = new NPOManagementContext())
            {
                var investor = await dbContext.InvestorInformations.Where(inv => inv.InvestorTierId == id).FirstOrDefaultAsync();

                var users = await dbContext.Users.Where(inv => inv.Id == investor.UserId).ToListAsync();

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
            }
        }

        public void AddUserType(UserInformationModel userInformationModel)
        {
            using(var dbContext = new NPOManagementContext())
            {
                var userTypes = dbContext.UserTypes.ToList();
                var user = dbContext.Users.Where(us => us.Id == userInformationModel.UserId).FirstOrDefault();
                foreach (var userType in userTypes)
                {
                    if( userType.Description == Convert.ToString( userInformationModel.UserTypeEnum ))
                    {
                        user.UserTypes.Add(userType);
                        dbContext.SaveChanges();

                    }
                    
                }
                
            }
        }
    }
}
