using Infrastructure.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.Common;
using Infrastructure.Repositories.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Configuration;

public static class InfrastructureConfig
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //MongoDB config
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDB"));

        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptionsSnapshot<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        // Đăng ký MongoDbContext sử dụng MongoClient
        services.AddSingleton<MongoDbContext>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoDbContext(Options.Create(settings));
        });
        
        // Identity Config
        services.AddIdentity<User, Role>(options =>
            {
                // Password config
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;

                // Password config when login fail
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

                // Email config 
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<Role>()
            .AddMongoDbStores<User, Role, Guid>(
                configuration["MongoDB:ConnectionString"],
                configuration["MongoDB:DatabaseName"]
            );
        
        // Inject Generic Repository
        services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}