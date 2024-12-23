using ForoWebApp.Models;
using MongoDB.Driver;

namespace ForoWebApp.Database.Repositories.Categories
{
	public class CategoriesRepository(DbContext context) : BaseRepository<Category>(context, "Categories"), ICategoriesRepository
	{
		public async Task<IAsyncCursor<Category>> FindAll()
		{
			return await Collection.FindAsync(_ => true);
		}

		public async Task<Category> FindById(int id)
		{
			return await Collection.FindAsync(x => x.Id == id).Result.FirstOrDefaultAsync();
		}
	}
}
