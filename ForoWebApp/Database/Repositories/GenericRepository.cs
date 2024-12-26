using ForoWebApp.Database.Documents;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ForoWebApp.Database.Repositories;

public abstract class GenericRepository<TDocument> : IGenericRepository where TDocument : IDocument
{
    protected IMongoCollection<TDocument> Collection { get; private set; }

    public GenericRepository(IMongoCollection<TDocument> collection)
    {
        Collection = collection;
    }

    public IQueryable<TDocument> AsQueryable()
    {
        return Collection.AsQueryable();
    }

    public Task<List<TDocument>> GetAllAsync()
    {
        return AsQueryable().ToListAsync();
    }

    public Task<TDocument> GetByIdAsync(int id)
    {
        return AsQueryable().FirstOrDefaultAsync(document => document.Id == id);
    }

    /*
    public async Task<TDocument> GetByIdAsync(string id)
    {
        return await Collection.FindAsync(element => );
    }*/
}
