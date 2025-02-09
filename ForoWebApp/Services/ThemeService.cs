using ForoWebApp.Database;
using ForoWebApp.Database.Documents;
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

    public Task<ThemeViewModel> GetThemeThreads(string themeId)
    {
        var threadCollection = _unitOfWork.ThreadsRepository.GetCollectionAsQueryable();
        var themeCollection = _unitOfWork.ThemesRepository.GetCollectionAsQueryable();
        var postsCollection = _unitOfWork.PostsRepository.GetCollectionAsQueryable();
        var usersCollection = _unitOfWork.UsersRepository.GetCollectionAsQueryable();

        var threadsQueryable =
            from theme in themeCollection
            join thread in threadCollection on theme.Id equals thread.ThemeId
            join post in postsCollection on thread.Id equals post.ThreadId into postsGroup
            where theme.Id == themeId
            group new { thread, postsGroup } by theme into groupedTheme
            select new ThemeViewModel
            {
                ThemeId = groupedTheme.Key.Id,
                ThemeTitle = groupedTheme.Key.Name,
                Threads = groupedTheme.AsQueryable().Select(groupedThreads => new
                {
                    Thread = groupedThreads.thread,
                    LastUpdateAt = groupedThreads.postsGroup.OrderByDescending(post => post.PostDate).First().PostDate,
                    TotalMessages = groupedThreads.postsGroup.Count()
                })
                .OrderByDescending(x => x.LastUpdateAt)
                .Select(x => new ThreadData
                {
                    Id = x.Thread.Id,
                    Title = x.Thread.Title,
                    LastUpdatedAt = x.LastUpdateAt,
                    TotalMessages = x.TotalMessages
                })
            };
        return threadsQueryable.FirstAsync();
    }
}
