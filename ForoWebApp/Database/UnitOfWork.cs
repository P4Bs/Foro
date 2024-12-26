using ForoWebApp.Database.Repositories.Categories;
using ForoWebApp.Database.Repositories.Messages;
using ForoWebApp.Database.Repositories.Threads;

namespace ForoWebApp.Database;

public class UnitOfWork
{
    private readonly DbContext _dbContext;
    private CategoriesRepository _categoriesRepository;
    private MessagesRepository _messagesRepository;
    private ThreadsRepository _threadsRepository;

    public CategoriesRepository CategoriesRepository => _categoriesRepository ?? new CategoriesRepository(_dbContext);
    public MessagesRepository MessagesRepository => _messagesRepository ?? new MessagesRepository(_dbContext);
    public ThreadsRepository ThreadsRepository => _threadsRepository ?? new ThreadsRepository(_dbContext);

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
}


/*
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

*/