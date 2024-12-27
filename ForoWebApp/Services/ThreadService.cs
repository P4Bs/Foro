using ForoWebApp.Database;
using ForoWebApp.Database.Documents;
using MongoDB.Driver.Linq;

namespace ForoWebApp.Services;

public class ThreadService(UnitOfWork unitOfWork)
{
	private readonly UnitOfWork _unitOfWork = unitOfWork;

	public async Task<IList<ForumThread>> GetThreadsByThemeId(string themeId)
	{
		var collection = _unitOfWork.ThreadsRepository.GetCollectionAsQueryable();

		return await collection.Where(thread => thread.ThemeId == themeId).ToListAsync();
	}

	public async Task<string> PublishThread(ForumThread thread)
	{
		return await _unitOfWork.ThreadsRepository.InsertAsync(thread);
	}
}
