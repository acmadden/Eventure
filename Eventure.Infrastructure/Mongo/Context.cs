using Eventure.Domain.Entities;
using Eventure.Infrastructure.Dao;
using Eventure.Infrastructure.Mongo.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Eventure.Infrastructure.Mongo
{
    public class Context
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public Context(IOptions<MongoDbSettings> options)
        {
            _client = new MongoClient(options.Value.ConnectionString);
            _database = _client.GetDatabase(options.Value.Database);
            Map();
        }

        public IMongoCollection<EventDao> Events { get => _database.GetCollection<EventDao>("events"); }

        private void Map()
        {
            BsonClassMap.RegisterClassMap<EventDao>(classMap =>
            {
                classMap.AutoMap();
            });

            BsonClassMap.RegisterClassMap<StoreOpened>(classMap =>
            {
                classMap.AutoMap();
            });

            BsonClassMap.RegisterClassMap<PhoneNumberChange>(classMap =>
            {
                classMap.AutoMap();
            });
        }
    }
}