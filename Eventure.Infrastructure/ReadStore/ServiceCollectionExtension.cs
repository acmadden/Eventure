using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using Eventure.Infrastructure.ReadStore.Repositories;
using Eventure.Infrastructure.ReadStore.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventure.Infrastructure.ReadStore
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddReadStore(this IServiceCollection services, IConfigurationSection section)
        {
            services.Configure<SqlServerSettings>(section);
            return services;
        }

        public static IServiceCollection AddReadStoreRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProjectionRepository<StoreOpenedEvent>, StoreProjectionRepository>();
            return services;
        }
    }
}