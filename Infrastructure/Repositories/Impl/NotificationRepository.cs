using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.Repositories.Common;
using Infrastructure.Repositories.Def;

namespace Infrastructure.Repositories.Impl;

public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
{
    public NotificationRepository(MongoDbContext context)
        : base(context, "Notifications") { }
}