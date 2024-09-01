using AutoMapper;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly NpomanagementContext _dbContext;

        public UserRepository(NpomanagementContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UserInformation, UserInformationModel>();
                cfg.CreateMap<UserInformationModel, UserInformation>();
                cfg.CreateMap<UserModel, User>();
                cfg.CreateMap<UsersGroupModel, UsersGroup>();
                cfg.CreateMap<UsersGroup, UsersGroupModel>();
                cfg.CreateMap<UserIdea, UserIdeaModel>();
                cfg.CreateMap<UserIdeaModel, UserIdea>();
                cfg.CreateMap<Account2User, Account2UserModel>();
                cfg.CreateMap<Account2UserModel, Account2User>();
                cfg.CreateMap<AccountModel, Account>();
                cfg.CreateMap<Account, AccountModel>();
            });
            _mapper = config.CreateMapper();
            _dbContext = context;
        }

        public async Task<UserModel> UpdateUserStatus(int userId, StatusEnumModel status)
        {
            if (userId <= 0)
            {
                return null;
            }

            var user = await _dbContext.Users.Where(us => us.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }

            if (user.StatusId != (int)status)
            {
                user.StatusId = (int)status;
                if(status == StatusEnumModel.Deleted)
                {
                    var userInfo = user.UserInformations.Where(info => info.UserId == user.Id).FirstOrDefault();
                    if (userInfo != null)
                    {
                        userInfo.DeletedAt = DateTime.Now;
                    }
                }

                await _dbContext.SaveChangesAsync();
                user = await _dbContext.Users.Where(us => us.Id == userId).FirstOrDefaultAsync();
            }

            return _mapper.Map<UserModel>(user);
        }

        public async Task UpdateGroupStatus(int userGroupId, GroupStatusEnum status)
        {
            var group = await _dbContext.UsersGroups.Where(gr => gr.Id == userGroupId).FirstOrDefaultAsync();
            if (group != null)
            {
                group.StatusId = (int)status;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var users = await _dbContext.Users.Include(usInfo => usInfo.UserInformations).ToListAsync();
            if (!users.Any())
            {
                return null;
            }

            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<List<UserModel>> GetUsersByCriteria(List<int>? statusIds)
        {
            var query = _dbContext.Users.Include(usInfo => usInfo.UserInformations).AsQueryable();
            if (statusIds != null && statusIds.Any())
            {
                query = query.Where(u => statusIds.Contains(u.StatusId));
            }

            var users = await query.ToListAsync();
            if (!users.Any())
            {
                return null;
            }

            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<UserModel> GetUserById(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }

            var user = await _dbContext.Users.Where(x => x.Id == userId).Include(usInfo => usInfo.UserInformations).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserModel>(user);
        }

        public async Task<List<UserModel>> GetUsersByInvestorTier(int investorTierId)
        {
            if (investorTierId <= 0)
            {
                return null;
            }

            var investor = await _dbContext.InvestorInformations.Include(us => us.User).Where(inv => inv.InvestorTierId == investorTierId).FirstOrDefaultAsync();
            if (investor == null)
            {
                return null;
            }

            var users = await _dbContext.Users.Where(inv => inv.Id == investor.UserId).ToListAsync();
            if (users == null || !users.Any())
            {
                return null;
            }

            var models = new List<UserModel>();
            foreach (var user in users)
            {
                var model = new UserModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    StatusId = user.StatusId
                };
                models.Add(model);
            }

            return models;
        }

        public async Task<List<UserIdeaModel>> GetAllIdeas()
        {
            var ideas = await _dbContext.UserIdeas.ToListAsync();
            if (!ideas.Any())
            {
                return null;
            }

            return _mapper.Map<List<UserIdeaModel>>(ideas);
        }

        public async Task<UserIdeaModel> AddUserIdea(UserIdeaModel userIdeaModel)
        {
            if (userIdeaModel == null)
            {
                return null;
            }

            var idea = _mapper.Map<UserIdea>(userIdeaModel);
            await _dbContext.UserIdeas.AddAsync(idea);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserIdeaModel>(idea);
        }

        public async Task<UserModel> ModifyUserCredentials(string email, string password, int userId, int statusId)
        {
            if (userId <= 0 || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                return null;
            }

            var user = await _dbContext.Users.Where(us => us.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }

            user.Email = email;
            user.Password = password;
            user.StatusId = statusId;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<UserModel>(user);
        }

        public async Task<bool> ModifyUserInfo(UserCredential userCredential)
        {
            if (userCredential == null)
            {
                return false;
            }
            var userInfo = await _dbContext.UserInformations.Where(us => us.UserId == userCredential.UserId).Include(us => us.User).FirstOrDefaultAsync();
            var user = await _dbContext.Users.Where(us => us.Id == userCredential.UserId).FirstOrDefaultAsync();
            if (userInfo == null || user == null)
            {
                return false;
            }

            userInfo.FirstName = userCredential.FirstName;
            user.StatusId = userCredential.StatusId;
            userInfo.LastName = userCredential.LastName;
            userInfo.RequestedUserRoleId = userCredential.RequestedUserRoleId;
            userInfo.PhoneNumber = userCredential.PhoneNumber;
            userInfo.UpdateDate = DateTime.UtcNow;
            userInfo.MiddleName = userCredential.MiddleName;
            userInfo.Metadata = userCredential.Metadata;
            userInfo.DateOfBirth = userCredential.DateOfBirth;
            userInfo.Gender = (int)userCredential.Gender;
            userInfo.UserImage = userCredential.UserImage;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var user = await _dbContext.Users.Where(user => user.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserModel>(user);
        }

        public async Task<List<SearchModel>> GetSearchResults(string searchParam, bool includeGroups)
        {
            if (string.IsNullOrEmpty(searchParam))
            {
                return null;
            }

            var users = await _dbContext.UserInformations
                .Where(u => u.FirstName.Contains(searchParam) || u.LastName.Contains(searchParam))
                .ToListAsync();

            var groups = new List<UsersGroup>();

            if (includeGroups)
            {
                groups = await _dbContext.UsersGroups
               .Where(g => g.GroupName.Contains(searchParam))
               .ToListAsync();
            }

            var result = new List<SearchModel>();

            users.ForEach(u => result.Add(new SearchModel()
            {
                Id = u.Id,
                Name = $"{u.FirstName} {u.LastName}",
                Type = SearchTypeEnum.User
            }));

            groups.ForEach(group => result.Add(new SearchModel()
            {
                Id = group.Id,
                Name = group.GroupName,
                Type = SearchTypeEnum.Group
            }));

            return result;
        }

        public async Task<UsersGroupModel> AddGroup(UsersGroupModel usersGroupModel)
        {
            if (usersGroupModel == null || string.IsNullOrEmpty(usersGroupModel.GroupName))
            {
                return null;
            }

            var usersGroup = _mapper.Map<UsersGroupModel, UsersGroup>(usersGroupModel);

            var users = await _dbContext.Users.Where(u => usersGroupModel.UserIds.Contains(u.Id)).ToListAsync();
            if (users.Any())
            {
                foreach (var user in users)
                {
                    if (user.StatusId == (int)StatusEnumModel.Active)
                    {
                        usersGroup.Users.Add(user);
                    }
                }
            }

            await _dbContext.UsersGroups.AddAsync(usersGroup);
            await _dbContext.SaveChangesAsync();

            var newUsersGroup = new UsersGroupModel()
            {
                Id = usersGroup.Id,
                GroupName = usersGroup.GroupName,
                CreatorId = usersGroup.CreatorId,
                Description = usersGroup.Description,
                UserIds = usersGroupModel.UserIds
            };

            return newUsersGroup;
        }

        public async Task AddUser(UserModel userModel)
        {
            if (userModel != null)
            {
                var user = _mapper.Map<UserModel, User>(userModel);
                user.StatusId = (int)StatusEnumModel.Pending;

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddUserInformation(UserInformationModel userInformationModel)
        {
            if (userInformationModel != null)
            {
                var userInfo = new UserInformation()
                {
                    RequestedUserRoleId = userInformationModel.RequestedUserRoleId,
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
            }
        }

        public async Task<List<UsersGroupModel>> GetAllGroups()
        {
            var groups = await _dbContext.UsersGroups.ToListAsync();
            if (!groups.Any())
            {
                return null;
            }

            return _mapper.Map<List<UsersGroupModel>>(groups);
        }

        public async Task<List<UsersGroupModel>> GetGroupsByName(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return null;
            }

            var groups = await _dbContext.UsersGroups.Where(group => group.GroupName.Contains(groupName)).ToListAsync();
            if (!groups.Any())
            {
                return null;
            }

            return _mapper.Map<List<UsersGroupModel>>(groups);
        }

        public async Task<UsersGroupModel> GetGroupById(int groupId)
        {
            if (groupId <= 0)
            {
                return null;
            }

            var group = await _dbContext.UsersGroups.Where(group => group.Id == groupId).FirstOrDefaultAsync();
            if (group == null)
            {
                return null;
            }

            return _mapper.Map<UsersGroupModel>(group);
        }

        public async Task<List<UserModel>> GetUsersByGroupId(int groupId)
        {
            if (groupId <= 0)
            {
                return null;
            }

            var group = await _dbContext.UsersGroups.Include(g => g.Users).Where(g => g.Id == groupId).FirstOrDefaultAsync();
            if (group == null || !group.Users.Any())
            {
                return null;
            }

            return _mapper.Map<List<UserModel>>(group.Users);
        }

        public async Task<List<UsersGroupModel>> GetGroupsForUser(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }

            var user = await _dbContext.Users.Include(g => g.Groups).Where(g => g.Id == userId).FirstOrDefaultAsync();
            if (user == null || !user.Groups.Any())
            {
                return null;
            }

            return _mapper.Map<List<UsersGroupModel>>(user.Groups);
        }

        public async Task<UsersGroupModel> DeleteUserFromGroup(int userId, int groupId)
        {
            if (userId <= 0 || groupId <= 0)
            {
                return null;
            }

            var group = await _dbContext.UsersGroups.Include(us => us.Users).Where(gr => gr.Id == groupId).FirstOrDefaultAsync();
            var user = await _dbContext.Users.Where(us => us.Id == userId).FirstOrDefaultAsync();
            if (group == null || user == null)
            {
                return null;
            }

            var userToRemove = group.Users.FirstOrDefault(user => user.Id == userId);
            if (userToRemove == null)
            {
                return null;
            }

            group.Users.Remove(userToRemove);
            await _dbContext.SaveChangesAsync();
            group = await _dbContext.UsersGroups.Include(us => us.Users).FirstOrDefaultAsync(gr => gr.Id == groupId);

            return _mapper.Map<UsersGroupModel>(group);
        }

        public async Task<bool> DeleteGroup(int groupId)
        {
            if (groupId <= 0)
            {
                return false;
            }

            var group = await _dbContext.UsersGroups.Include(us => us.Users).FirstOrDefaultAsync(gr => gr.Id == groupId);
            if (group == null)
            {
                return false;
            }
            group.Users.Clear();
            await _dbContext.SaveChangesAsync();

            _dbContext.Remove(group);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddUserToGroup(int userId, int groupId)
        {
            if (userId <= 0 || groupId <= 0)
            {
                return false;
            }

            var group = await _dbContext.UsersGroups.Include(us => us.Users).FirstOrDefaultAsync(gr => gr.Id == groupId);
            var user = await _dbContext.Users.Where(us => us.Id == userId).FirstOrDefaultAsync();
            if (user == null || group == null || group.Users.Contains(user))
            {
                return false;
            }

            group.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Account2UserModel>> GetUsersInfoAccount(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }

            var account2user = await _dbContext.Account2Users.Where(us => us.UserId == userId).Include(acc => acc.Account).ToListAsync();
            if (account2user == null || !account2user.Any())
            {
                return null;
            }


            return _mapper.Map<List<Account2UserModel>>(account2user);
        }
    }
}