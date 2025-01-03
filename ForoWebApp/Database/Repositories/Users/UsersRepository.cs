using ForoWebApp.Database.Documents;

namespace ForoWebApp.Database.Repositories.Users;

public class UsersRepository(DbContext context) : GenericRepository<User>(context.Users)
{
	public async Task<(string, bool)> TryRegister(User user)
	{
		try
		{
			await Collection.InsertOneAsync(user);
		}
		catch (Exception)
        {
			return (user.Id, false);
		}

		return (user.Id, true);
	}
}
