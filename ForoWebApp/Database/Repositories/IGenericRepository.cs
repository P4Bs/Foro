using ForoWebApp.Database.Documents;

namespace ForoWebApp.Database.Repositories;

public interface IGenericRepository<TDocument> where TDocument : IDocument
{
    IQueryable<TDocument> AsQueryable();

    Task<List<TDocument>> GetAllAsync();

    Task<TDocument> GetByIdAsync(int id);

    Task<TDocument> InsertAsync(TDocument document);

    Task<TDocument> UpdateAsync(TDocument id);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}
