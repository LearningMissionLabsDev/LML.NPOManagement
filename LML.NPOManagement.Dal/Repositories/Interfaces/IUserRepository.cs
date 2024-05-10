using LML.NPOManagement.Common;
using LML.NPOManagement.Common.Model;

namespace LML.NPOManagement.Dal.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsers();
        Task<List<UserModel>> GetUsersByInvestorTier(int userId);
        Task<UserModel> GetUserById(int userId);
        Task<List<UserIdeaModel>> GetAllIdeas();
        Task<UserIdeaModel> AddUserIdea(UserIdeaModel userIdeaModel);
        Task<UserModel> ModifyUserCredentials(string email, string password, int userId, int statusId);
        Task<bool> ModifyUserInfo(UserInformationModel userInformation);
        Task<UserModel> UpdateUserStatus(int userGroupId, StatusEnumModel status);
        Task UpdateGroupStatus(int userId, GroupStatusEnum status);
        Task<UsersGroupModel> DeleteUserFromGroup(int userId, int groupId);
        Task<bool> DeleteGroup(int groupId);
        Task<List<Account2UserModel>> GetUsersInfoAccount(int userId);
        Task<UserModel> GetUserByEmail(string email);
        Task<List<SearchModel>> GetSearchResults(string searchParam, bool includeGroups);
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
