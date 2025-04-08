using Infrastructure.Context;
using Infrastructure.Repositories.Def;
using Infrastructure.Repositories.Impl;

namespace Infrastructure.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly MongoDbContext _context;

    public UnitOfWork(MongoDbContext context, ICategoryRepository categoryRepository, IRefreshTokenBlackListRepository refreshTokenBlackListRepository, 
        INotificationRepository notificationRepository, ITeamRepository teamRepository, ITaskItemRepository taskItemRepository)
    {
        _context = context;
        CategoryRepository = categoryRepository;
        RefreshTokenBlackListRepository = refreshTokenBlackListRepository;
        NotificationRepository = notificationRepository;
        TeamRepository = teamRepository;
        TaskItemRepository = taskItemRepository;
    }

    public ICategoryRepository CategoryRepository { get; }
    public IRefreshTokenBlackListRepository RefreshTokenBlackListRepository { get; }
    public INotificationRepository NotificationRepository { get; }
    public ITeamRepository TeamRepository { get; }
    public ITaskItemRepository TaskItemRepository { get; }

    public Task CommitAsync()
    {
        return Task.CompletedTask;
    }
}