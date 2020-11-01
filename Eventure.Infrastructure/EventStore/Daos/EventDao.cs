using System;
using Eventure.Domain;
using MongoDB.Bson;

namespace Eventure.Infrastructure.EventStore.Dao
{
    public class EventDao
    {
        public ObjectId Id { get; set; }
        public Guid AggregateId { get; set; }
        public string Aggregate { get; set; }
        public int Version { get; set; }
        public IDomainEvent Data { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}