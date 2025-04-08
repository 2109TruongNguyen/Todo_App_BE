using Application.Mapper;
using Application.Services.Common;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration;

public static class ApplicationConfig
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddSingleton<UploadImageService>();
            return services;
        }
    }