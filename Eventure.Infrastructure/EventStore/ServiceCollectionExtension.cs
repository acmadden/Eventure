using System;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using Eventure.Infrastructure.EventStore.Daos;
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
            BsonClassMap.RegisterClassMap<Store>();
            BsonClassMap.RegisterClassMap<StoreOpenedEvent>();
            BsonClassMap.RegisterClassMap<StorePhoneNumberChangedEvent>();
            services.Configure<EventStoreSettings>(section);
            services.AddScoped<Context>();
            return services;
        }

        public static IServiceCollection AddEventStoreRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IEventStoreRepository<>), typeof(EventStoreRepository<>));
            return services;
        }
    }
}