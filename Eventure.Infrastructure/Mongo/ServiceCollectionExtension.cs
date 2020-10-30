using System;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using Eventure.Infrastructure.Mongo.Repositories;
using Eventure.Infrastructure.Mongo.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventure.Infrastructure.Mongo
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMongoDbContext(this IServiceCollection services, IConfigurationSection section)
        {
            services.Configure<MongoDbSettings>(section);
            services.AddScoped<Context>();
            return services;
        }

        public static IServiceCollection AddMongoRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Store, Guid>, StoreRepository>();
            return services;
        }
    }
}