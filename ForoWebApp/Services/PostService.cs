using ForoWebApp.Database;
using ForoWebApp.Database.Documents;

namespace ForoWebApp.Services;

public class PostService(UnitOfWork unitOfWork)
{
	private readonly UnitOfWork _unitOfWork = unitOfWork;

	public async Task<string> PublishPost(Post newMessage)
	{
		return await _unitOfWork.PostsRepository.InsertAsync(newMessage);
	}
}
