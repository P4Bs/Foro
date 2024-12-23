using MongoDB.Driver;
using Thread = ForoWebApp.Models.Thread;

namespace ForoWebApp.Database.Repositories.Threads
{
	public interface IThreadsRepository
	{
		Task InsertOneAsync(Thread entity);

		Task<IAsyncCursor<Thread>> FindAllByThemeIdAsync(int themeId);

		Task<Thread> FindByIdAsync(int threadId);

		Task CloseThreadById(int threadId, UpdateDefinition<Thread>[] closureUpdates);
	}
}
