using ForoWebApp.Models;
using MongoDB.Driver;

namespace ForoWebApp.Database.Repositories.Users
{
	public class UsersRepository(DbContext context) : BaseRepository<User>(context, "Users"), IUsersRepository
	{
		public async void InsertOneAsync(User user)
		{
			await Collection.InsertOneAsync(user);
		}

		public async Task<IAsyncCursor<User>> GetAllUsers()
		{
			return await Collection.FindAsync(_ => true);
		}

		public async Task<User> FindUserById(int id)
		{
			return await Collection.FindAsync(user => user.Id == id).Result.FirstOrDefaultAsync();
		}

		public async Task<bool> DeleteUserById(int id)
		{
			var deleteResult = await Collection.DeleteOneAsync(user => user.Id == id);

			return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
		}

		public async void UpdateUser(int userId, UpdateDefinition<User>[] updateDefinitions)
		{
			await Collection.UpdateOneAsync(
				filter: user => user.Id == userId,
				update: Builders<User>.Update.Combine(updateDefinitions)
			);
		}
	}
}
