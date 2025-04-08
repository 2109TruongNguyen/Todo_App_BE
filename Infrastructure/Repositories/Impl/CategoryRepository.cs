using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Repositories.Common;
using Infrastructure.Repositories.Def;

namespace Infrastructure.Repositories.Impl;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private MongoDbContext _context;
    
    public CategoryRepository(MongoDbContext context) 
        : base(context, "Categories")
    {
        _context = context;
    }
}