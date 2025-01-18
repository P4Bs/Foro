using ForoWebApp.Database.Documents;

namespace ForoWebApp.Database.Repositories.Themes;

public class ThemesRepository(DbContext dbContext) : GenericRepository<Theme>(dbContext.Themes)
{		
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
