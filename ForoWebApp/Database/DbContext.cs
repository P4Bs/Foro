using ForoWebApp.Database.Documents;
using ForoWebApp.Models.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Thread = ForoWebApp.Database.Documents.Thread;

namespace ForoWebApp.Database;

public class DbContext
{
	private readonly IMongoDatabase _database;
    private IMongoCollection<Category> _categories;
    private IMongoCollection<Message> _messages;
	private IMongoCollection<Theme> _themes;
    private IMongoCollection<Thread> _threads;
    private IMongoCollection<User> _users;

    public IMongoCollection<Category> Categories => _categories ??= GetCollection<Category>("categories");
    public IMongoCollection<Message> Messages => _messages ??= GetCollection<Message>("messages");
    public IMongoCollection<Theme> Themes => _themes ??= GetCollection<Theme>("themes");
    public IMongoCollection<Thread> Threads => _threads ??= GetCollection<Thread>("threads");
    public IMongoCollection<User> Users => _users ??= GetCollection<User>("users");

    public DbContext(IOptionsMonitor<DbSettings> options)
	{
        var client = new MongoClient(options.CurrentValue.ConnectionString);
        _database = client.GetDatabase(options.CurrentValue.DatabaseName);
    }

	protected IMongoCollection<T> GetCollection<T>(string collectionName)
	{
		return _database.GetCollection<T>(collectionName);
	}
}
