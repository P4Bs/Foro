using ForoWebApp.Database.Documents;

namespace ForoWebApp.Database.Repositories.Threads;

public class ThreadsRepository(DbContext dbContext) : GenericRepository<ForumThread>(dbContext.Threads)
{
    /*
	public async Task<int> InsertOneAsync(Thread thread)
	{
		await _unitOfWork.ThreadsCollection.InsertOneAsync(thread);
		return thread.Id;
	}

	public async Task<IAsyncCursor<Thread>> FindAllByThemeIdAsync(int themeId)
	{
		return await _unitOfWork.ThreadsCollection.FindAsync(thread => thread.ThemeId == themeId && thread.IsClosed == false);
	}

	public async Task<Thread> FindByIdAsync(int threadId)
	{
		return await _unitOfWork.ThreadsCollection.FindAsync(thread => thread.Id == threadId && thread.IsClosed == false).Result.FirstOrDefaultAsync();
	}

	public async Task CloseThreadById(int threadId, UpdateDefinition<Thread>[] closureUpdates)
	{
		await _unitOfWork.ThreadsCollection.UpdateOneAsync(
			filter: thread => thread.Id == threadId,
			update: Builders<Thread>.Update.Combine(closureUpdates)
		);
	}*/
}
