using ForoWebApp.Database;
using ForoWebApp.Mappers;

namespace ForoWebApp.Services;

public class PostService(UnitOfWork unitOfWork)
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;

    public Task<string> CreatePost(Models.Domain.Post model)
    {
        Database.Documents.Post newPostDocument = PostMapper.ToDatabaseDocument(model);
        return _unitOfWork.PostsRepository.InsertAsync(newPostDocument);
    }
}
