using ForoWebApp.Models;
using MongoDB.Driver;

namespace ForoWebApp.Database.Repositories.Messages
{
	public class MessagesRepository(DbContext context) : BaseRepository<Message>(context, "Messages"), IMessagesRepository
	{
		public async Task InsertOneAsync(Message message)
		{
			await Collection.InsertOneAsync(message);
		}

		public async Task<IAsyncCursor<Message>> FindAllByThreadIdAsync(int threadId)
		{
			return await Collection.FindAsync(message => message.ThreadId == threadId);
		}

		public async Task<Message> FindByIdAsync(int messageId)
		{
			return await Collection.FindAsync(message => message.Id == messageId).Result.FirstOrDefaultAsync();
		}

		public async Task UpdateByIdAsync(int messageId, UpdateDefinition<Message>[] messageUpdates)
		{
			await Collection.UpdateOneAsync(
				filter: message => message.Id == messageId,
				update: Builders<Message>.Update.Combine(messageUpdates)
			);
		}

		public async Task<bool> DeleteByIdAsync(int messageId)
		{
			var deleteResult = await Collection.DeleteOneAsync(message => messageId == message.Id);

			return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
		}
	}
}
