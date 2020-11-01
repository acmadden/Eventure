using Eventure.Domain.Entities;
using Eventure.Infrastructure.EventStore.Dao;
using Eventure.Infrastructure.EventStore.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Eventure.Infrastructure.EventStore
{
    public class Context
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public Context(IOptions<MongoDbSettings> options)
        {
            _client = new MongoClient(options.Value.ConnectionString);
            _database = _client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<EventDao> Events { get => _database.GetCollection<EventDao>("events"); }
    }
}