using Amazon.Runtime.Internal.Util;
using AutoMapper;
using Grpc.Core;
using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using LML.NPOManagement.Dal;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Bll.Services
{
    public class UsersGroupService : IUsersGroupService
    {
        private IMapper _mapper;
        private readonly INPOManagementContext _dbContext;

        public UsersGroupService(INPOManagementContext dbContext)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserModel, User>();
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UserInformationModel, UserInformation>();
                cfg.CreateMap<UserInformation, UserInformationModel>();
                cfg.CreateMap<UsersGroupModel, UsersGroup>();
                cfg.CreateMap<UsersGroup, UsersGroupModel>();
            });
            _mapper = config.CreateMapper();
            _dbContext = dbContext;
        }
        public async Task<List<UserModel>> GetUserByUsername(string userName, bool showGroupsOnly)
        {
            List<User> users = new();
            int userId = 420;

            if (!showGroupsOnly)
            {
                users = await _dbContext.Users.ToListAsync();
            }
            else
            {
                var groupIds = _dbContext.Users
                 .Where(u => u.Id == userId)
                 .SelectMany(u => u.Groups.Select(g => g.Id))
                 .ToList();

                users = _dbContext.Users
                 .Where(u => u.Groups.Any(g => groupIds.Contains(g.Id)))
                 .ToList();
            }

            List<UserModel> result = new();

            foreach (var user in users)
            {
                var userInfo = await _dbContext.UserInformations.Where(u => u.UserId == user.Id).FirstOrDefaultAsync();
                if (userInfo != null)
                {
                    string fullName = userInfo.FirstName + " " + userInfo.LastName;
                    if (fullName.StartsWith(userName, StringComparison.OrdinalIgnoreCase))
                    {
                        var userModel = _mapper.Map<User, UserModel>(user);
                        result.Add(userModel);
                    }
                }
            }
            return result;
        }
        public async Task<UsersGroupModel> AddUsersGroup(UsersGroupModel usersGroupModel, List<int> userIds)
        {
            var users = await _dbContext.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
            if (users.Count < 2)
            {
                return null;
            }
            var usersGroup = _mapper.Map<UsersGroupModel, UsersGroup>(usersGroupModel);

            usersGroup.Users = users;
            usersGroup.CreatorId = usersGroupModel.CreatedByUserId;
            
            await _dbContext.UsersGroups.AddAsync(usersGroup);
            await _dbContext.SaveChangesAsync();

            var newUsersGroup = new UsersGroupModel()
            {
                Id = usersGroup.Id,
                GroupName = usersGroup.GroupName,
                CreatedAt = usersGroup.DateCreated,
                CreatedByUserId = usersGroup.CreatorId,
                Description = usersGroup.Description,
            };
            return newUsersGroup;
        }
    }
}
