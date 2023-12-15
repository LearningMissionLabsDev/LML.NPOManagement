using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BC = BCrypt.Net.BCrypt;

namespace LML.NPOManagement.Bll.Services
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        //private readonly IBaseRepository _baseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IInvestorRepository _investorRepository;
        private readonly IAccountRepository _accountRepository;
        public UserService(IUserRepository userRepository, IInvestorRepository investorRepository, IAccountRepository accountRepository/*, IBaseRepository baseRepository*/)
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
                cfg.CreateMap<UserModel, UserInformation>();
                cfg.CreateMap<UserInformation,UserModel>();
            });
            _mapper = config.CreateMapper();
            //_baseRepository = baseRepository;
            _userRepository = userRepository;
            _investorRepository = investorRepository;
            _accountRepository = accountRepository;
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.Users.Where(us => us.Id == id).FirstOrDefault();
            user.Status = Convert.ToString(StatusEnumModel.Closed);
            _userRepository.SaveChanges();
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            List<UserModel> userModels = new List<UserModel>();
            var users = await _userRepository.Users.ToListAsync();
            foreach (var user in users)
            {
                var userModel = _mapper.Map<User, UserModel>(user);
                userModels.Add(userModel);
            }
            return userModels;
        }

        public async Task<List<UserInformationModel>> GetUserByName(UserInformationModel userSearch)
        {
            var allUsers = await _userRepository.UserInformations.Where(name => (EF.Functions.Like(name.FirstName, $"%{userSearch.FirstName}%") || EF.Functions.Like(name.LastName, $"%{userSearch.LastName}%" ))).ToListAsync();

            if (allUsers == null)
            {
                return null;
            }
            var userInfoModelList = new List<UserInformationModel>();

            foreach (var user in allUsers)
            {
                var userInfoModel = new UserInformationModel
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
                userInfoModelList.Add(userInfoModel);
            }
            return userInfoModelList;
        }

        public async Task<UserModel> GetUserById(int id)
        {
            var user = await _userRepository.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                var userModel = _mapper.Map<User, UserModel>(user);
                return userModel;
            }
            return null;
        }

        public async Task<UserModel> Login(UserModel userModel, IConfiguration configuration)
        {
            var user = await _userRepository.Users.FirstOrDefaultAsync(m => m.Email == userModel.Email);
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
            var user = await _userRepository.Users.Where(us => us.Id == id).FirstOrDefaultAsync();
            user.Email = userModel.Email;
            user.Password = BC.HashPassword(userModel.Password);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<UserModel> Registration(UserModel userModel, IConfiguration configuration)
        {
            var user = await _userRepository.Users.FirstOrDefaultAsync(m => m.Email == userModel.Email);

            if (user == null)
            {
                userModel.Password = BC.HashPassword(userModel.Password);
                var addUser = _mapper.Map<UserModel, User>(userModel);
                await _userRepository.Users.AddAsync(addUser);
                await _userRepository.SaveChangesAsync();
                var newUser = await _userRepository.Users.FirstOrDefaultAsync(us => us.Email == userModel.Email);
                var newUserModel = _mapper.Map<User, UserModel>(newUser);
                newUserModel.Token = TokenCreationHelper.GenerateJwtToken(newUserModel, configuration);
                newUserModel.Password = null;
                return newUserModel;
            }
            else if (user.Status == Convert.ToString(StatusEnumModel.Closed))
            {
                return null;//to do handle this condition differently
            }
            return null;
        }

        public async Task<int> UserInformationRegistration(UserInformationModel userInformationModel, IConfiguration configuration)
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
            await _userRepository.UserInformations.AddAsync(userInfo);
            await _userRepository.SaveChangesAsync();
            if (userInformationModel.UserTypeEnum == UserTypeEnum.Investor)
            {
                _investorRepository.InvestorInformations.Add(new InvestorInformation()
                {
                    UserId = userInformationModel.UserId,
                    InvestorTierId = Convert.ToInt16(InvestorTierEnum.Basic),
                });
                await _userRepository.SaveChangesAsync();
            }

            return userInfo.Id;
        }

        public async Task<List<UserModel>> GetUsersByRole(int id)
        {
            var userRole = await _userRepository.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();
            var users = await _userRepository.Users.Where(ro => ro.Roles.Contains(userRole)).ToListAsync();
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
            var account = await _accountRepository.Accounts.Where(acc => acc.Id == id).FirstOrDefaultAsync();
            var users = await _userRepository.Users.Where(acc => acc.Accounts.Contains(account)).ToListAsync();
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
            var investor = await _investorRepository.InvestorInformations.Where(inv => inv.InvestorTierId == id).FirstOrDefaultAsync();

            var users = await _userRepository.Users.Where(inv => inv.Id == investor.UserId).ToListAsync();

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
            var userTypes = await _userRepository.UserTypes.ToListAsync();
            var user = await _userRepository.Users.Where(us => us.Id == userInformationModel.UserId).FirstOrDefaultAsync();
            foreach (var userType in userTypes)
            {
                if (userType.Description == Convert.ToString(userInformationModel.UserTypeEnum))
                {
                    user.UserTypes.Add(userType);
                    await _userRepository.SaveChangesAsync();
                    var newUserType = _mapper.Map<UserType, UserTypeModel>(userType);
                    return newUserType;
                }
            }
            return null;
        }

        public async Task<UserModel> ActivationUser(string token, IConfiguration configuration)
        {
            var newUser = TokenCreationHelper.ValidateJwtToken(token, configuration);
            var user = await _userRepository.Users.Where(us => us.Id == newUser.Id).FirstOrDefaultAsync();
            user.Status = StatusEnumModel.Activ.ToString();
            await _userRepository.SaveChangesAsync();
            var userModel = _mapper.Map<User, UserModel>(user);
            return userModel;
        }

        public async Task<bool> ModifyUserInfo(UserInformationModel userInformationModel, int id)
        {
            var userInfo = await _userRepository.UserInformations.Where(us => us.UserId == id).FirstOrDefaultAsync();

            if (userInfo == null)
            {
                return false;
            }

            var user = await _userRepository.Users.Where(us => us.Id == id).FirstOrDefaultAsync();

            userInfo.UserId = id;
            userInfo.FirstName = userInformationModel.FirstName;
            userInfo.LastName = userInformationModel.LastName;
            userInfo.PhoneNumber = userInformationModel.PhoneNumber;
            userInfo.UpdateDate = DateTime.UtcNow;
            userInfo.MiddleName = userInformationModel.MiddleName;
            userInfo.Metadata = userInformationModel.Metadata;
            userInfo.DateOfBirth = userInformationModel.DateOfBirth;
            userInfo.Gender = (int)userInformationModel.Gender;

            var userTypes = await _userRepository.UserTypes.ToListAsync();

            foreach (var userType in userTypes)
            {
                if (userType.Description == Convert.ToString(userInformationModel.UserTypeEnum) &&
                     user.UserTypes.Where(us => us.Description == Convert.ToString(userInformationModel.UserTypeEnum)) == null)
                {
                    user.UserTypes.Add(userType);
                }
            }
            await _userRepository.SaveChangesAsync();
            return true;
        }
    }
}
