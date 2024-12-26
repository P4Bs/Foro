using MongoDB.Driver;
using Thread = ForoWebApp.Database.Documents.Thread;

namespace ForoWebApp.Database.Repositories.Threads
{
	public interface IThreadsRepository
	{
		Task<int> InsertOneAsync(Thread thread);

		Task<IAsyncCursor<Thread>> FindAllByThemeIdAsync(int themeId);

		Task<Thread> FindByIdAsync(int threadId);

		Task CloseThreadById(int threadId, UpdateDefinition<Thread>[] closureUpdates);
	}
}
