using ForoWebApp.Database.Documents;
using MongoDB.Driver;

namespace ForoWebApp.Database.Repositories.Categories
{
	public class CategoriesRepository(DbContext context) : ICategoriesRepository
	{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
		private readonly UnitOfWork _unitOfWork;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

		public async Task<IAsyncCursor<Category>> FindAll()
		{
			return await _unitOfWork.CategoriesCollection.FindAsync(_ => true);
		}

		public async Task<Category> FindById(int id)
		{
			return await _unitOfWork.CategoriesCollection.FindAsync(x => x.Id == id).Result.FirstOrDefaultAsync();
		}
	}
}
