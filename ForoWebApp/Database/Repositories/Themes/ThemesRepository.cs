using ForoWebApp.Models;
using MongoDB.Driver;

namespace ForoWebApp.Database.Repositories.Themes
{
	public class ThemesRepository(DbContext context) : BaseRepository<Theme>(context, "Themes"), IThemesRepository
	{
		public async Task InsertOneAsync(Theme theme)
		{
			await Collection.InsertOneAsync(theme);
		}

		public async Task<IAsyncCursor<Theme>> FindAllAsync()
		{
			return await Collection.FindAsync(_ => true);
		}

		public async Task<Theme> FindByIdAsync(int id)
		{
			return await Collection.FindAsync(x => x.Id == id).Result.FirstOrDefaultAsync();
		}
	}
}
