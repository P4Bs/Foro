using ForoWebApp.Database.Documents;
using MongoDB.Driver;

namespace ForoWebApp.Database.Repositories.Users
{
	public interface IUsersRepository
	{
		void InsertOneAsync(User user);

		Task<IAsyncCursor<User>> GetAllUsers();

		Task<User> FindUserById(int id);

		Task<bool> DeleteUserById(int id);

		void UpdateUser(int userId, UpdateDefinition<User>[] updateDefinitions);
	}
}
