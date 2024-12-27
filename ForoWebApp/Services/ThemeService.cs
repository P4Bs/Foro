using ForoWebApp.Database;

namespace ForoWebApp.Services;

public class ThemeService(UnitOfWork unitOfWork)
{
	private readonly UnitOfWork _unitOfWork = unitOfWork;
}
