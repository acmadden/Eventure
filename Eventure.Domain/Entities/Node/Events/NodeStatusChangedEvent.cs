using System;
using Eventure.Domain.ValueObjects;

namespace Eventure.Domain.Entities
{
    public class NodeStatusChangedEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public NodeStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public NodeStatusChangedEvent(Guid aggregateId, NodeStatus status)
        {
            AggregateId = aggregateId;
            Status = status;
            CreatedAt = DateTime.UtcNow;
        }
    }
}