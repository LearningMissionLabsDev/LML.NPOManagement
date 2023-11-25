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
            });
            _mapper = config.CreateMapper();
            _dbContext = context;
        }

        public void DeleteUser(int id)
        {
            var user = _dbContext.Users.Where(us => us.Id == id).FirstOrDefault();
            user.Status = Convert.ToString(StatusEnumModel.Closed);
            _dbContext.SaveChanges();
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            List<UserModel> userModels = new List<UserModel>();
            var users = await _dbContext.Users.ToListAsync();
            foreach (var user in users)
            {
                var userModel = _mapper.Map<User, UserModel>(user);
                userModels.Add(userModel);
            }
            return userModels;
        }

        public async Task <UserModel> GetUserById(int id)
        {
            var user = await _dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                var userModel = _mapper.Map<User,UserModel>(user);
                return userModel;
            }
            return null;
        }

        public async Task<UserModel> Login(UserModel userModel, IConfiguration configuration)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(m => m.Email == userModel.Email);
            if (user != null && BC.Verify(userModel.Password, user.Password))
            {
                var userModelMapper = _mapper.Map<User, UserModel>(user);
                userModelMapper.Password = null;
                userModelMapper.Token = TokenCreationHelper.GenerateJwtToken(userModelMapper, configuration);
                return userModelMapper;
            }

            return null;
        }

        public async Task<bool> ModifyUser(UserModel userModel, int id)
        {
            var user = await _dbContext.Users.Where(us => us.Id == id).FirstOrDefaultAsync();            
            user.Email = userModel.Email;
            user.Password = BC.HashPassword(userModel.Password);                
            await _dbContext.SaveChangesAsync();          
            return true;
        }

        public async Task<UserModel> Registration(UserModel userModel, IConfiguration configuration)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(m => m.Email == userModel.Email);
       
            if (user == null)
            {             
                userModel.Password = BC.HashPassword(userModel.Password);
                var addUser = _mapper.Map<UserModel, User>(userModel);
                await _dbContext.Users.AddAsync(addUser);
                await _dbContext.SaveChangesAsync();
                var newUser = await _dbContext.Users.FirstOrDefaultAsync(us => us.Email == userModel.Email);
                // if ( newUser == null ) { return null; } <i think>
                var newUserModel = _mapper.Map<User, UserModel>(newUser);
                newUserModel.Token = TokenCreationHelper.GenerateJwtToken(newUserModel, configuration);
                newUserModel.Password = null;
                return newUserModel;                    
            }
            else if(user.Status == Convert.ToString(StatusEnumModel.Closed))
            {
                return null;//to do handle this condition differently
            }
            return null;
        }

        public async Task<int> UserInformationRegistration(UserInformationModel userInformationModel,IConfiguration configuration )
        {
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
            await _dbContext.UserInformations.AddAsync(userInfo);
            await _dbContext.SaveChangesAsync();
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
        }

        public async Task<List<UserModel>> GetUsersByRole(int id)
        {
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
        }

        public async Task<List<UserModel>> GetUsersByAccount(int id)
        {
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
        }

        public async Task<List<UserModel>> GetUsersByInvestorTier(int id)
        {
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
        }

        public async Task<UserTypeModel> AddUserType(UserInformationModel userInformationModel)
        {
            var userTypes = await _dbContext.UserTypes.ToListAsync();
            var user = await _dbContext.Users.Where(us => us.Id == userInformationModel.UserId).FirstOrDefaultAsync();            foreach (var userType in userTypes)
            {
                if( userType.Description == Convert.ToString( userInformationModel.UserTypeEnum ))
                {
                    user.UserTypes.Add(userType);
                    await _dbContext.SaveChangesAsync();
                    var newUserType = _mapper.Map<UserType, UserTypeModel>(userType);
                    return newUserType;
                }
            }
            return null;
        }

        public async Task<UserModel> ActivationUser(string token,IConfiguration configuration)
        {
            var newUser = TokenCreationHelper.ValidateJwtToken(token, configuration);
            var user = await _dbContext.Users.Where(us => us.Id == newUser.Id).FirstOrDefaultAsync();
            user.Status = StatusEnumModel.Activ.ToString();
            await _dbContext.SaveChangesAsync();
            var userModel = _mapper.Map<User,UserModel>(user);
            return userModel;
        }

        public async Task<bool> ModifyUserInfo(UserInformationModel userInformationModel, int id)
        {
            var userInfo = await _dbContext.UserInformations.Where(us => us.UserId == id).FirstOrDefaultAsync();            

            if (userInfo == null)
            {
                return false;
            }

            var user = await _dbContext.Users.Where(us => us.Id == id).FirstOrDefaultAsync();

            userInfo.UserId = id;
            userInfo.FirstName = userInformationModel.FirstName;
            userInfo.LastName = userInformationModel.LastName;
            userInfo.PhoneNumber = userInformationModel.PhoneNumber;
            userInfo.UpdateDate = DateTime.UtcNow;
            userInfo.MiddleName = userInformationModel.MiddleName;
            userInfo.Metadata = userInformationModel.Metadata;
            userInfo.DateOfBirth = userInformationModel.DateOfBirth;
            userInfo.Gender = (int) userInformationModel.Gender;
           
            var userTypes = await _dbContext.UserTypes.ToListAsync();

            foreach (var userType in userTypes)
            {
                if ( userType.Description == Convert.ToString(userInformationModel.UserTypeEnum) &&
                     user.UserTypes.Where(us => us.Description == Convert.ToString(userInformationModel.UserTypeEnum)) == null)
                {
                    user.UserTypes.Add(userType);            
                }
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
