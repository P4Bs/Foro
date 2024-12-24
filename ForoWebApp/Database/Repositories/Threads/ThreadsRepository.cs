using MongoDB.Driver;
using Thread = ForoWebApp.Models.Thread;

namespace ForoWebApp.Database.Repositories.Threads
{
	public class ThreadsRepository(DbContext context) : BaseRepository<Thread>(context, "Threads"), IThreadsRepository
	{
		public async Task<int> InsertOneAsync(Thread thread)
		{
			await Collection.InsertOneAsync(thread);
			return thread.Id;
		}

		public async Task<IAsyncCursor<Thread>> FindAllByThemeIdAsync(int themeId)
		{
			return await Collection.FindAsync(thread => thread.ThemeId == themeId && thread.IsClosed == false);
		}

		public async Task<Thread> FindByIdAsync(int threadId)
		{
			return await Collection.FindAsync(thread => thread.Id == threadId && thread.IsClosed == false).Result.FirstOrDefaultAsync();
		}

		public async Task CloseThreadById(int threadId, UpdateDefinition<Thread>[] closureUpdates)
		{
			await Collection.UpdateOneAsync(
				filter: thread => thread.Id == threadId,
				update: Builders<Thread>.Update.Combine(closureUpdates)
			);
		}
	}
}
