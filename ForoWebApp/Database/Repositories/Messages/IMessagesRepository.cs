using ForoWebApp.Database.Documents;
using ForoWebApp.Models.ViewModels;
using MongoDB.Driver;

namespace ForoWebApp.Database.Repositories.Messages
{
	public interface IMessagesRepository
	{
		Task<int> InsertOneAsync(Message message);

		Task<IAsyncCursor<Message>> FindAllByThreadIdAsync(int threadId);

		Task<IAsyncCursor<MessageViewModel>> FindAllByThreadIdWithUserData(int threadId);

		Task<Message> FindByIdAsync(int messageId);

		Task UpdateByIdAsync(int messageId, UpdateDefinition<Message>[] messageUpdates);

		Task<bool> DeleteByIdAsync(int messageId);
	}
}
