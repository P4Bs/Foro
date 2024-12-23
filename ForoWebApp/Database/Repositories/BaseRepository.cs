using MongoDB.Driver;

namespace ForoWebApp.Database.Repositories
{
	public class BaseRepository<T>(DbContext context, string collectionName)
	{
		protected readonly IMongoCollection<T> Collection = context.GetCollection<T>(collectionName);
	}
}
