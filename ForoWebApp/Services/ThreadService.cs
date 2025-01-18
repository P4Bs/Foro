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

	public Task<ThreadViewModel> GetThreadMessages(string threadId)
	{
		var messageCollection = _unitOfWork.MessagesRepository.GetCollectionAsQueryable();
		var threadsCollection = _unitOfWork.ThreadsRepository.GetCollectionAsQueryable();
		var usersCollection = _unitOfWork.UsersRepository.GetCollectionAsQueryable();

		var messagesQuery = from thread in threadsCollection
							join message in messageCollection on thread.Id equals message.ThreadId
							join user in usersCollection on message.UserId equals user.Id
							where thread.Id == threadId
							group new { message, user } by thread into groupedThread
							select new ThreadViewModel
							{
								ThreadId = groupedThread.Key.Id,
								ThreadName = groupedThread.Key.Title,
								Messages = groupedThread.AsQueryable().Select(
									groupedMessage => new MessageViewModel
									{
										Id = groupedMessage.message.Id,
										UserId = groupedMessage.user.Id,
										UserName = groupedMessage.user.Name,
										Content = groupedMessage.message.Content,
										PublishingDate = groupedMessage.message.PublishingDate,
									}
								)
							};

		return messagesQuery.FirstOrDefaultAsync();
	}
}
