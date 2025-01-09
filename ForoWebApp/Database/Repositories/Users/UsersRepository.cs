using ForoWebApp.Database.Documents;
using ForoWebApp.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
			return (null, false);
		}

		return (user.Id, true);
	}

	public Task<User> FindUserByLogin(UserLoginModel loginModel)
	{
		return Collection.AsQueryable().FirstOrDefaultAsync(user => user.Email == loginModel.Email && user.Password == loginModel.Password);
	}
}
