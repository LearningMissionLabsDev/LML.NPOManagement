using AutoMapper;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal.Models;
using Microsoft.AspNetCore.Mvc;
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
                cfg.CreateMap<UserInformation, UserInformationModel>();
                cfg.CreateMap<UserType, UserTypeModel>();
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UserInformationModel, UserInformation>();
                cfg.CreateMap<UserTypeModel, UserType>();
                cfg.CreateMap<UserModel, User>();
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
                string encPass = BC.HashPassword(userModel.Password);

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

                if (user == null || user.Status != Convert.ToString(StatusEnumModel.Closed))
                {
                    string encPass = BC.HashPassword(userModel.Password);
                    var addUser = _mapper.Map<UserModel, User>(userModel);
                    dbContext.Users.Add(addUser);
                    dbContext.SaveChanges();
                    var newUser = _mapper.Map<User,UserModel>(addUser);
                    newUser.Password = null;
                    return newUser;
                }
                return null;
            }           
        }
        public async Task<int> UserInformationRegistration(UserInformationModel userInformationModel)
        {
            using(var dbContext = new NPOManagementContext())
            {
                var userInfo = _mapper.Map<UserInformationModel, UserInformation>(userInformationModel);
                dbContext.UserInformations.Add(userInfo);
                dbContext.SaveChanges();
                return userInfo.Id;
            }
        }
    }
}
