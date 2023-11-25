using LML.NPOManagement.Bll.Model;

namespace LML.NPOManagement.Bll.Interfaces
{
	public interface IMessageService
	{
		Task<MessageModel> AddMessage(MessageModel messageModel);
		Task<List<MessageModel>> GetMessages(string currentUserEmail, string currentSobes);
    }
}

