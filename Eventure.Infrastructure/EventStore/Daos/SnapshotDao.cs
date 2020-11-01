using System;
using Eventure.Domain;
using MongoDB.Bson;

namespace Eventure.Infrastructure.EventStore.Daos
{
    public class SnapshotDao
    {
        public ObjectId Id { get; set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
        public ISnapshotData Data { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}