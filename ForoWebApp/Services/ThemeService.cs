using ForoWebApp.Database;
using ForoWebApp.Database.Documents;

namespace ForoWebApp.Services;

public class ThemeService(UnitOfWork unitOfWork)
{
	private readonly UnitOfWork _unitOfWork = unitOfWork;

	public async Task<IList<Theme>> GetThemes()
	{
		return await _unitOfWork.ThemesRepository.GetAllAsync(); 
	}
}
