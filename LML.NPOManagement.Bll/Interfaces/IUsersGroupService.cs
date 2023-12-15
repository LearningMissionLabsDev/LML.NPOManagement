using LML.NPOManagement.Bll.Model;
using Microsoft.Extensions.Configuration;

namespace LML.NPOManagement.Bll.Interfaces
{
    public interface IUsersGroupService
    {
        Task<List<UserModel>> GetUserByUsername(string username, bool showGroupsOnly);
        Task<UsersGroupModel> AddUsersGroup(UsersGroupModel model, List<int> userIds);
    }
}
