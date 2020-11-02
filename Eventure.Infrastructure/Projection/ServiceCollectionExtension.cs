using Eventure.Application.Projection.ReadModels;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using Eventure.Infrastructure.Projection.Repositories;
using Eventure.Infrastructure.Projection.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventure.Infrastructure.Projection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddProjection(this IServiceCollection services, IConfigurationSection section)
        {
            services.Configure<SqlServerSettings>(section);
            return services;
        }

        public static IServiceCollection AddProjectionRepositories(this IServiceCollection services)
        {
            services.AddTransient<IWriteProjectionRepository<StoreOpenedEvent>, StoreWriteProjectionRepository>();
            services.AddTransient<IWriteProjectionRepository<StorePhoneNumberChangedEvent>, StoreWriteProjectionRepository>();
            services.AddTransient<IWriteProjectionRepository<StoreClosedEvent>, StoreWriteProjectionRepository>();
            services.AddTransient<IReadProjectionRepository<StoreReadModel>, StoreReadProjectionRepository>();
            return services;
        }
    }
}