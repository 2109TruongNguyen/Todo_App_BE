using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Repositories.Common;
using Infrastructure.Repositories.Def;

namespace Infrastructure.Repositories.Impl;

public class TeamRepository : GenericRepository<Team>, ITeamRepository
{
    public TeamRepository(MongoDbContext context)
        : base(context, "Teams") { }
}