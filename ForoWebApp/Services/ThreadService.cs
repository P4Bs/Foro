using ForoWebApp.Database;
using ForoWebApp.Database.Constants;
using ForoWebApp.Database.Documents;
using ForoWebApp.Models.ViewModels;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ForoWebApp.Services;

public class ThreadService(UnitOfWork unitOfWork)
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;

    public Task<string> PublishThread(ForumThread thread)
    {
        return _unitOfWork.ThreadsRepository.InsertAsync(thread);
    }

    public async Task<ThreadViewModel> GetThreadPosts(string threadId, int? pageNumber = null, int? postsPerPage = Constants.PostsPerPage)
    {
        var postsCollection = _unitOfWork.PostsRepository.GetCollectionAsQueryable();
        var threadsCollection = _unitOfWork.ThreadsRepository.GetCollectionAsQueryable();
        var usersCollection = _unitOfWork.UsersRepository.GetCollectionAsQueryable();

        var postsQuery = from post in postsCollection
                         join user in usersCollection on post.UserId equals user.Id
                         where post.ThreadId == threadId
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
        int definedPostsPerPage = Constants.PostsPerPage;
        if (postsPerPage.HasValue && postsPerPage.Value > 0)
        {
            definedPostsPerPage = postsPerPage.Value;
        }
        int totalPages = (int) Math.Ceiling( (double) totalPosts / definedPostsPerPage );
        int requestedPage = 1;
        if (pageNumber.HasValue)
        {
            if(pageNumber.Value > totalPages)
            {
                requestedPage = totalPages;
            }
            else if(pageNumber.Value > 1)
            {
                requestedPage = pageNumber.Value;
            }
        }

        var postsList = await postsQuery.Skip( (requestedPage - 1) * definedPostsPerPage ).Take( definedPostsPerPage ).ToListAsync();

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
            TotalPostsPerPage = definedPostsPerPage,
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
