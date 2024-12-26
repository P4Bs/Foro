using ForoWebApp.Database.Documents;
using MongoDB.Driver;

namespace ForoWebApp.Database.Repositories.Categories
{
	public interface ICategoriesRepository
	{
		Task<IAsyncCursor<Category>> FindAll();
		Task<Category> FindById(int id);
	}
}
