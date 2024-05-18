using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;
using Microsoft.Extensions.Configuration;


namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsers();
        Task<List<UserModel>> GetUsersByInvestorTier(int id);
        Task<UserModel> GetUserById(int userId);
        Task<UserModel> GetUserByEmail(string email);
        Task<List<UserIdeaModel>> GetAllIdeas();
        Task<UserIdeaModel> AddUserIdea(UserIdeaModel userIdeaModel);
        Task<UserModel> ModifyUserCredentials(string email, string password, int userId);
        Task<bool> ModifyUserInfo(UserInformationModel userInformationModel);
        Task<UserModel> DeleteUser(int userId);
        Task<bool> DeleteUserFromGroup(int userId, int groupId);
        Task<bool> DeleteGroup(int groupId);
        Task<UserModel> ActivationUser(string token, IConfiguration configuration);
        Task<bool> AddUserToGroup(int userId,int groupId);
        Task<UserModel> Login(UserModel userModel, IConfiguration configuration);
        Task<UserModel> Registration(UserModel userModel, IConfiguration configuration);
        Task<int?> UserInformationRegistration(UserInformationModel userInformationModel, IConfiguration configuration);
        Task<List<SearchModel>> GetSearchResults(string searchParam, bool includeGroups);
        Task<UsersGroupModel> CreateGroup(UsersGroupModel userGroupModel);
        Task<List<UsersGroupModel>> GetAllGroups();
        Task<List<UsersGroupModel>> GetGroupsByName(string groupName);
        Task<UsersGroupModel> GetGroupById(int groupId);
        Task<List<UserModel>> GetUsersByGroupId(int groupId);
        Task<List<UsersGroupModel>> GetGroupsForUser(int userId);
    }
}
