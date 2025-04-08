using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Repositories.Common;
using Infrastructure.Repositories.Def;

namespace Infrastructure.Repositories.Impl;

public class RefreshTokenBlackListRepository : GenericRepository<RefreshTokenBlackList> , IRefreshTokenBlackListRepository
{
    public RefreshTokenBlackListRepository(MongoDbContext context)
        : base(context, "Teams") { }
}