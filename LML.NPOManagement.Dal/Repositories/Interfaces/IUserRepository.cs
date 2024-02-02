using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using LML.NPOManagement.Dal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsers();
        Task<List<UserModel>> GetUsersByInvestorTier(int userId);
        Task<List<UserModel>> GetUsersByAccount(int userId);
        Task<UserModel> GetUserById(int userId);
        Task<List<UserInformationModel>> GetUsersByName(UserInformationModel name);
        Task<UserModel> ModifyUserCredentials(string email, string password, int userId, int statusId);
        Task<bool> ModifyUserInfo(UserInformationModel userInformation, int userId);
        Task UpdateUserStatus(int userId, StatusEnumModel status);
        Task UpdateGroupStatus(int userId, GroupStatusEnum status);
        Task DeleteUserFromGroup(int userId, int groupId);
        Task DeleteGroup(int groupId);
        Task<UserModel> GetUserByEmail(string email);
        Task<List<SearchModel>> GetSearchResult(string firstChars, bool includeGroups);
        Task<UsersGroupModel> AddGroup(UsersGroupModel groupModel);
        Task<bool> AddUserToGroup(int userId,int groupId);
        Task<List<UsersGroupModel>> GetAllGroups();
        Task<List<UsersGroupModel>> GetGroupsByName(string groupName);
        Task<UsersGroupModel> GetGroupById(int groupId);
        Task<List<UserModel>> GetUsersByGroupId(int groupId);
        Task<List<UsersGroupModel>> GetGroupsForUser(int userId);
        Task AddUser(UserModel userModel);
        Task AddUserInformation(UserInformationModel userInformationModel);
    }
}
