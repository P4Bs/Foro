using ForoWebApp.Database.Documents;
using MongoDB.Driver;

namespace ForoWebApp.Database.Repositories.Categories;

public class CategoriesRepository: GenericRepository<Category>
{
    public CategoriesRepository(DbContext context) : base(context.Categories)
    {
    }
}
