namespace Eventure.Infrastructure.EventStore.Settings
{
    public class EventStoreSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public int SnapshotInterval { get; set; }
    }
}