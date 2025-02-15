using ForoWebApp.Constants;
using ForoWebApp.Database;
using ForoWebApp.Database.Documents;
using ForoWebApp.Helpers;
using ForoWebApp.Mappers;
using ForoWebApp.Models.ViewModels;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ForoWebApp.Services;

public class ThreadService(UnitOfWork unitOfWork)
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;

    public Task<string> CreateThread(Models.Domain.ForumThread model)
    {
        ForumThread newForumThreadDocument = ForumThreadMapper.ToDatabaseDocument(model);
        return _unitOfWork.ThreadsRepository.InsertAsync(newForumThreadDocument);
    }

    public async Task<ThreadViewModel> GetThreadPosts(string threadId, int? pageNumber = null, int? postsPerPage = DatabaseConstants.PostsPerPage)
    {
        var postsCollection = _unitOfWork.PostsRepository.GetCollectionAsQueryable();
        var threadsCollection = _unitOfWork.ThreadsRepository.GetCollectionAsQueryable();
        var usersCollection = _unitOfWork.UsersRepository.GetCollectionAsQueryable();

        var postsQuery =    from post in postsCollection
                            where post.ThreadId == threadId
                            join user in usersCollection on post.UserId equals user.Id
                            orderby post.PostDate
                            select new PostData
                            {
                                Id = post.Id,
                                UserId = user.Id,
                                UserName = user.Name,
                                Message = post.Content,
                                PostDate = post.PostDate
                            };

        int totalPosts = await postsQuery.CountAsync();
        int itemsPerPage = PageHelper.ResolveItemsPerPage(postsPerPage, DatabaseConstants.PostsPerPage);
        int totalPages = (int)Math.Ceiling((double)totalPosts / itemsPerPage);
        int requestedPage = PageHelper.ResolveRequestedPage(pageNumber, totalPages);

        var postsList = await postsQuery
                                .Skip((requestedPage - 1) * itemsPerPage)
                                .Take(itemsPerPage)
                                .ToListAsync();

        var threadData = await
                            (from thread in threadsCollection
                            where thread.Id == threadId
                            select new
                            {
                                thread.Id,
                                thread.Title,
                                thread.IsClosed
                            }).FirstOrDefaultAsync();

        return new ThreadViewModel
        {
            ThreadId = threadData.Id,
            ThreadName = threadData.Title,
            IsClosed = threadData.IsClosed,
            Posts = postsList,
            TotalPosts = totalPosts,
            PostsPerPage = itemsPerPage,
            TotalPages = totalPages,
            CurrentPage = requestedPage
        };
    }

    public async Task<bool> CloseThread(string threadId)
    {
        UpdateDefinition<ForumThread>[] threadUpdateDefinitions =
        [
            Builders<ForumThread>.Update.Set(thread => thread.IsClosed, true),
            Builders<ForumThread>.Update.Set(thread => thread.ClosureDate, DateTime.UtcNow)
        ];

        UpdateResult result = await _unitOfWork.ThreadsRepository.UpdateAsync(threadId, threadUpdateDefinitions);

        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
}
