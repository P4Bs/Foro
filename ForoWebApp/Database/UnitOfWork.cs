using ForoWebApp.Database.Documents;
using MongoDB.Driver;
using Thread = ForoWebApp.Database.Documents.Thread;

namespace ForoWebApp.Database
{
	public class UnitOfWork(IMongoDatabase database)
	{
		private readonly IMongoDatabase _database = database;
		public IMongoCollection<Authentication> AuthenticationsCollection => _database.GetCollection<Authentication>("Authentications");
		public IMongoCollection<User> UsersCollection => _database.GetCollection<User>("Users");
		public IMongoCollection<Category> CategoriesCollection => _database.GetCollection<Category>("Categories");
		public IMongoCollection<Theme> Themes => _database.GetCollection<Theme>(nameof(Themes));
		public IMongoCollection<Thread> ThreadsCollection => _database.GetCollection<Thread>("Threads");
		public IMongoCollection<Message> MessagesCollection => _database.GetCollection<Message>("Messages");
	}
}
