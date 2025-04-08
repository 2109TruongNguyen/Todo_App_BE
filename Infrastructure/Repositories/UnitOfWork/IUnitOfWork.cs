using Infrastructure.Repositories.Def;

namespace Infrastructure.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
    IRefreshTokenBlackListRepository RefreshTokenBlackListRepository { get; }
    INotificationRepository NotificationRepository { get; }
    ITeamRepository TeamRepository { get; }
    ITaskItemRepository TaskItemRepository { get; }
    Task CommitAsync();
}