using ForoWebApp.Database;
using ForoWebApp.Database.Documents;
using ForoWebApp.Models.ViewModels;
using MongoDB.Driver.Linq;

namespace ForoWebApp.Services;

public class ThreadService(UnitOfWork unitOfWork)
{
	private readonly UnitOfWork _unitOfWork = unitOfWork;

	public Task<string> PublishThread(ForumThread thread)
	{
		return _unitOfWork.ThreadsRepository.InsertAsync(thread);
	}

	public Task<ThreadViewModel> GetThreadPosts(string threadId)
	{
		var postsCollection = _unitOfWork.PostsRepository.GetCollectionAsQueryable();
		var threadsCollection = _unitOfWork.ThreadsRepository.GetCollectionAsQueryable();
		var usersCollection = _unitOfWork.UsersRepository.GetCollectionAsQueryable();

		var postsQuery = from thread in threadsCollection
							join post in postsCollection on thread.Id equals post.ThreadId
							join user in usersCollection on post.UserId equals user.Id
							where thread.Id == threadId
							group new { post, user } by thread into groupedThread
							select new ThreadViewModel
							{
								ThreadId = groupedThread.Key.Id,
								ThreadName = groupedThread.Key.Title,
								IsClosed = groupedThread.Key.IsClosed,
								Posts = groupedThread.AsQueryable().Select(
									groupedMessage => new PostData
									{
										Id = groupedMessage.post.Id,
										UserId = groupedMessage.user.Id,
										UserName = groupedMessage.user.Name,
										Message = groupedMessage.post.Content,
										PostDate = groupedMessage.post.PostDate,
									}
								)
							};

		return postsQuery.FirstOrDefaultAsync();
	}
}
