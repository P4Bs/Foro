using ForoWebApp.Constants;
using ForoWebApp.Database;
using ForoWebApp.Database.Documents;
using ForoWebApp.Helpers;
using ForoWebApp.Models.ViewModels;
using MongoDB.Driver.Linq;

namespace ForoWebApp.Services;

public class ThemeService(UnitOfWork unitOfWork)
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;

    public Task<List<Theme>> GetThemes()
    {
        return _unitOfWork.ThemesRepository.GetAllAsync();
    }

    public async Task<ThemeViewModel> GetTheme(string themeId, int? pageNumber = null, int? threadsPerPage = DatabaseConstants.ThreadsPerPage)
    {
        var threadCollection = _unitOfWork.ThreadsRepository.GetCollectionAsQueryable();
        var themeCollection = _unitOfWork.ThemesRepository.GetCollectionAsQueryable();
        var postsCollection = _unitOfWork.PostsRepository.GetCollectionAsQueryable();
        var usersCollection = _unitOfWork.UsersRepository.GetCollectionAsQueryable();

        var threadsQuery = from thread in threadCollection
                           where thread.ThemeId == themeId
                           join post in postsCollection on thread.Id equals post.ThreadId into postsGroup
                           let lastPost = postsGroup.OrderByDescending(post => post.PostDate).FirstOrDefault()
                           join user in usersCollection on lastPost.UserId equals user.Id
                           select new ThreadData
                           {
                               Id = thread.Id,
                               Title = thread.Title,
                               LastUpdatedAt = lastPost.PostDate,
                               LastUpdateByUser = user.Name,
                               TotalMessages = postsGroup.Count()
                           };

        int totalThreads = await threadsQuery.CountAsync();
        int itemsPerPage = PageHelper.ResolveItemsPerPage(threadsPerPage, DatabaseConstants.ThreadsPerPage);
        int totalPages = (int)Math.Ceiling((double)totalThreads / itemsPerPage);
        int requestedPage = PageHelper.ResolveRequestedPage(pageNumber, totalPages);

        var threadsList = await threadsQuery
                                .Skip((requestedPage - 1) * itemsPerPage)
                                .Take(itemsPerPage)
                                .OrderByDescending(thread => thread.LastUpdatedAt)
                                .ToListAsync();

        var themeData = await
                            (from theme in themeCollection
                             where theme.Id == themeId
                             select new
                             {
                                 theme.Id,
                                 theme.Name
                             }).FirstOrDefaultAsync();

        return new ThemeViewModel
        {
            ThemeId = themeData.Id,
            ThemeTitle = themeData.Name,
            Threads = threadsList
        };
    }
}
