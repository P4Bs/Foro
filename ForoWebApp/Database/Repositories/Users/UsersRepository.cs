using ForoWebApp.Database.Documents;

namespace ForoWebApp.Database.Repositories.Users;

public class UsersRepository(DbContext context) : GenericRepository<User>(context.Users)
{
	
}
