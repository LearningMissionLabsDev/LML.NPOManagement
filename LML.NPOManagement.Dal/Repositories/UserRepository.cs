using AutoMapper;
using FluentEmail.Core;
using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Models;
using LML.NPOManagement.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LML.NPOManagement.Dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        IMapper _mapper;
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
            var user = await _dbContext.Users.Where(us => us.Id == userId).FirstOrDefaultAsync();
            if(user == null)
            {
                return null;
            }
            if (user.StatusId == (int)status)
            {
                return null;   
            }
            user.StatusId = (int)status;
            await _dbContext.SaveChangesAsync();
            user = await _dbContext.Users.Where(us => us.Id == userId).FirstOrDefaultAsync();

            return _mapper.Map<UserModel>(user);
        }

        public async Task UpdateGroupStatus(int userId, GroupStatusEnum status)
        {
            var group = await _dbContext.UsersGroups.Where(gr => gr.Id == userId).FirstOrDefaultAsync();

            if (group != null)
            {
                group.StatusId = (int)status;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var users = await _dbContext.Users.ToListAsync();

            if (users.Count < 1)
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
            var user = await _dbContext.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

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
            var investor = await _dbContext.InvestorInformations.Where(inv => inv.InvestorTierId == investorTierId).FirstOrDefaultAsync();
            if (investor == null)
            {
                return null;
            }
            var users = await _dbContext.Users.Where(inv => inv.Id == investor.UserId).ToListAsync();

            if (users.Count < 1)
            {
                return null;
            }

            return _mapper.Map<List<UserModel>>(users);
        }

        public async Task<List<UserIdeaModel>> GetAllIdea()
        {
            var ideas = await _dbContext.UserIdeas.ToListAsync();

            if (ideas.Count < 1)
            {
                return null;
            }
            return _mapper.Map<List<UserIdeaModel>>(ideas);
        }

        public async Task<UserIdeaModel> AddUserIdea(UserIdeaModel userIdeaModel)
        {
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

        public async Task<bool> ModifyUserInfo(UserInformationModel userInformationModel, int userId)
        {
            if (userId <= 0)
            {
                return false;
            }
            var userInfo = await _dbContext.UserInformations.Where(us => us.UserId == userId).FirstOrDefaultAsync();

            if (userInfo == null)
            {
                return false;
            }
            var user = await _dbContext.Users.Where(us => us.Id == userId).FirstOrDefaultAsync();

            userInfo.UserId = userId;
            userInfo.FirstName = userInformationModel.FirstName;
            userInfo.LastName = userInformationModel.LastName;
            userInfo.PhoneNumber = userInformationModel.PhoneNumber;
            userInfo.UpdateDate = DateTime.UtcNow;
            userInfo.MiddleName = userInformationModel.MiddleName;
            userInfo.Metadata = userInformationModel.Metadata;
            userInfo.DateOfBirth = userInformationModel.DateOfBirth;
            userInfo.Gender = (int)userInformationModel.Gender;

            //var userTypes = await _dbContext.UserTypes.ToListAsync();

            //foreach (var userType in userTypes)
            //{
            //    if (userType.Description == Convert.ToString(userInformationModel.UserTypeEnum) &&
            //         user.UserTypes.Where(us => us.Description == Convert.ToString(userInformationModel.UserTypeEnum)) == null)
            //    {
            //        user.UserTypes.Add(userType);
            //    }
            //}
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

            List<SearchModel> result = new();

            foreach (var user in users)
            {
                var searchModel = new SearchModel()
                {
                    Id = user.Id,
                    Name = user.FirstName + " " + user.LastName,
                    Type = SearchTypeEnum.User
                };

                result.Add(searchModel);
            }

            foreach (var group in groups)
            {
                var searchModel = new SearchModel()
                {
                    Id = group.Id,
                    Name = group.GroupName,
                    Type = SearchTypeEnum.Group
                };

                result.Add(searchModel);
            }

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

            if (users.Count > 0)
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
                var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userInformationModel.UserId);
                //var userType = await _dbContext.UserTypes.Where(usType => usType.Description == Convert.ToString(userInformationModel.UserTypeEnum)).FirstOrDefaultAsync();
                
                //    if(user != null && userType != null)
                //    {
                //        user.UserTypes.Add(userType);
                //    }

                await _dbContext.UserInformations.AddAsync(userInfo);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<UsersGroupModel>> GetAllGroups()
        {
            var groups = await _dbContext.UsersGroups.ToListAsync();

            if (groups.Count < 1)
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

            if (groups.Count < 1)
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
            var group = await _dbContext.UsersGroups.Include(g => g.Users).FirstOrDefaultAsync(g => g.Id == groupId);

            if (group == null)
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
            var user = await _dbContext.Users.Include(g => g.Groups).FirstOrDefaultAsync(g => g.Id == userId);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<List<UsersGroupModel>>(user.Groups);
        }

        public async Task<UsersGroupModel> DeleteUserFromGroup(int userId, int groupId)
        {
            if (userId <= 0 || groupId <= 0)
            {
                throw new ArgumentException("Delete failed");
            }
            var group = await _dbContext.UsersGroups.Include(us => us.Users).FirstOrDefaultAsync(gr => gr.Id == groupId);
            var user = await _dbContext.Users.FirstOrDefaultAsync(us => us.Id == userId);
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

            if (user == null || group == null)
            {
                return false;
            }
            if (group.Users.Contains(user))
            {
                return false;
            }
            group.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            if (!group.Users.Contains(user))
            {
                return false;
            }
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
