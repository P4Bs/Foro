using ForoWebApp.Models;
using MongoDB.Driver;

namespace ForoWebApp.Database.Repositories.Messages
{
	public interface IMessagesRepository
	{
		Task InsertOneAsync(Message message);

		Task<IAsyncCursor<Message>> FindAllByThreadIdAsync(int threadId);

		Task<Message> FindByIdAsync(int messageId);

		Task UpdateByIdAsync(int messageId, UpdateDefinition<Message>[] messageUpdates);

		Task<bool> DeleteByIdAsync(int messageId);
	}
}
