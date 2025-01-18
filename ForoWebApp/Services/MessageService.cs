using ForoWebApp.Database;
using ForoWebApp.Database.Documents;
using ForoWebApp.Models.ViewModels;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ForoWebApp.Services;

public class MessageService(UnitOfWork unitOfWork)
{
	private readonly UnitOfWork _unitOfWork = unitOfWork;

	public async Task<IList<MessageViewModel>> GetThreadMessages(string threadId)
	{
		var messagesQueryableCollection = _unitOfWork.MessagesRepository.GetCollectionAsQueryable();
		var usersQueryableCollection = _unitOfWork.UsersRepository.GetCollectionAsQueryable();

		var query = from message in messagesQueryableCollection
					 join user in usersQueryableCollection on message.UserId equals user.Id
					 where message.ThreadId == threadId
					 select new MessageViewModel
					 {
						 Id = message.Id,
						 UserId = user.Id,
						 UserName = user.Name,
						 Content = message.Content,
						 PublishingDate = message.PublishingDate
					 };

		return await query.ToListAsync();
	}

	public async Task<string> PublishMessage(Message newMessage)
	{
		return await _unitOfWork.MessagesRepository.InsertAsync(newMessage);
	}

}
