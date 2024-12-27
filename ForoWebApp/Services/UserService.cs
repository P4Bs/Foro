using ForoWebApp.Database;

namespace ForoWebApp.Services;

public class UserService(UnitOfWork unitOfWork)
{
	private readonly UnitOfWork _unitOfWork = unitOfWork;


}
