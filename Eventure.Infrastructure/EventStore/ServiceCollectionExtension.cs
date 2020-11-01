using System;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using Eventure.Infrastructure.EventStore.Dao;
using Eventure.Infrastructure.EventStore.Repositories;
using Eventure.Infrastructure.EventStore.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace Eventure.Infrastructure.EventStore
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEventStore(this IServiceCollection services, IConfigurationSection section)
        {
            BsonClassMap.RegisterClassMap<EventDao>();
            BsonClassMap.RegisterClassMap<StoreOpened>();
            BsonClassMap.RegisterClassMap<PhoneNumberChange>();
            services.Configure<MongoDbSettings>(section);
            services.AddScoped<Context>();
            return services;
        }

        public static IServiceCollection AddEventStoreRepositories(this IServiceCollection services)
        {
            services.AddTransient<IEventStoreRepository<Store, Guid>, StoreRepository>();
            return services;
        }
    }
}