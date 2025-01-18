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

        var threadsQueryable = from theme in themeCollection
                     join thread in threadCollection on theme.Id equals thread.ThemeId
                     where theme.Id == themeId
                     group thread by theme into groupedTheme
                     select new ThemeViewModel
                     {
                         ThemeId = groupedTheme.Key.Id,
                         ThemeTitle = groupedTheme.Key.Name,
                         Threads = groupedTheme.Select(thread => new ThreadData
                         {
                             Id = thread.Id,
                             Title = thread.Title,
                             LastUpdatedAt = thread.LastUpdateAt,
                             LastUpdateUsername = thread.LastUpdateUsername,
                             TotalMessages = thread.TotalMessages
                         })
                     };

        return threadsQueryable.FirstAsync();
    }
}
