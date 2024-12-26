using ForoWebApp.Database.Documents;

namespace ForoWebApp.Database.Repositories.Messages;

public class MessagesRepository : GenericRepository<Message>
{
    public MessagesRepository(DbContext dbContext) : base(dbContext.Messages)
    {
    }

    /*
	private readonly UnitOfWork _unitOfWork;

	public async Task<int> InsertOneAsync(Message message)
	{
		await _unitOfWork.MessagesCollection.InsertOneAsync(message);
		return message.Id;
	}

	public async Task<IAsyncCursor<Message>> FindAllByThreadIdAsync(int threadId)
	{
		return await _unitOfWork.MessagesCollection.FindAsync(message => message.ThreadId == threadId);
	}

	public async Task<IAsyncCursor<MessageViewModel>> FindAllByThreadIdWithUserData(int threadId)
	{
		var messagesQueryableCollection = _unitOfWork.MessagesCollection.AsQueryable();
		var usersQueryableCollection = _unitOfWork.UsersCollection.AsQueryable();

		var result = from message in messagesQueryableCollection
					 join user in usersQueryableCollection on message.UserId equals user.Id
					 where message.ThreadId == threadId
					 select new MessageViewModel
					 {
						 Id = message.Id,
						 ThreadId = message.ThreadId,
						 UserId = user.Id,
						 UserName = user.Name,
						 UserProfilePicture = user.ProfilePicture,
						 MessageContent = message.MessageContent,
						 PublishingDate = message.PublishingDate,
						 EditionDate = message.EditionDate
					 };

		return await result.ToCursorAsync();
	}

	public async Task<Message> FindByIdAsync(int messageId)
	{
		var messageCursor = await _unitOfWork.MessagesCollection.FindAsync(message => message.Id == messageId);
		return await messageCursor.FirstOrDefaultAsync();
	}

	public async Task UpdateByIdAsync(int messageId, UpdateDefinition<Message>[] messageUpdates)
	{
		await _unitOfWork.MessagesCollection.UpdateOneAsync(
			filter: message => message.Id == messageId,
			update: Builders<Message>.Update.Combine(messageUpdates)
		);
	}

	public async Task<bool> DeleteByIdAsync(int messageId)
	{
		var deleteResult = await _unitOfWork.MessagesCollection.DeleteOneAsync(message => messageId == message.Id);

		return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
	}*/

}
