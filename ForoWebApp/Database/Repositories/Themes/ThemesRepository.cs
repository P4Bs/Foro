using ForoWebApp.Database.Documents;
using MongoDB.Driver;

namespace ForoWebApp.Database.Repositories.Themes
{
	public class ThemesRepository : IThemesRepository
	{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
		private readonly UnitOfWork _unitOfWork;
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
		
		//public async Task InsertOneAsync(Theme theme)
		//{
		//	await _unitOfWork.ThemesCollection.InsertOneAsync(theme);
		//}

		//public async Task<IAsyncCursor<Theme>> FindAllAsync()
		//{
		//	//TODO: EDITAR ESTO PARA AGRUPAR POR CATEGORIAS
		//	return await _unitOfWork.ThemesCollection.FindAsync(_ => true);
		//}

		//public async Task<Theme> FindByIdAsync(int id)
		//{
		//	return await _unitOfWork.ThemesCollection.FindAsync(x => x.Id == id).Result.FirstOrDefaultAsync();
		//}
	}
}
