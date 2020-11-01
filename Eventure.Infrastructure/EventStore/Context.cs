using Eventure.Infrastructure.EventStore.Daos;
using Eventure.Infrastructure.EventStore.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Eventure.Infrastructure.EventStore
{
    public class Context
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public Context(IOptions<EventStoreSettings> options)
        {
            _client = new MongoClient(options.Value.ConnectionString);
            _database = _client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<EventDao> Events { get => _database.GetCollection<EventDao>("events"); }

        public IMongoCollection<SnapshotDao> Snapshots { get => _database.GetCollection<SnapshotDao>("snapshots"); }
    }
}