using ForoWebApp.Database.Repositories.Posts;
using ForoWebApp.Database.Repositories.Themes;
using ForoWebApp.Database.Repositories.Threads;
using ForoWebApp.Database.Repositories.Users;

namespace ForoWebApp.Database;

public class UnitOfWork(DbContext dbContext)
{
    private readonly DbContext _dbContext = dbContext;
    private readonly PostsRepository _postsRepository;
    private readonly ThemesRepository _themesRepository;
    private readonly ThreadsRepository _threadsRepository;
    private readonly UsersRepository _usersRepository;

    public PostsRepository PostsRepository => _postsRepository ?? new PostsRepository(_dbContext);
    public ThemesRepository ThemesRepository => _themesRepository ?? new ThemesRepository(_dbContext);
    public ThreadsRepository ThreadsRepository => _threadsRepository ?? new ThreadsRepository(_dbContext);
    public UsersRepository UsersRepository => _usersRepository ?? new UsersRepository(_dbContext);
}
