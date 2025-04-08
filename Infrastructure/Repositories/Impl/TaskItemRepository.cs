using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Repositories.Common;
using Infrastructure.Repositories.Def;

namespace Infrastructure.Repositories.Impl;

public class TaskItemRepository : GenericRepository<TaskItem>, ITaskItemRepository
{
    public TaskItemRepository(MongoDbContext context)
        : base(context, "TaskItems") { }
}