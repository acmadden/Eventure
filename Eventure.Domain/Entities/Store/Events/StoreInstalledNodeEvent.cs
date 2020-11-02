using System;

namespace Eventure.Domain.Entities
{
    public class StoreInstalledNodeEvent : IDomainEvent
    {
        public Guid NodeId { get; set; }
        public DateTime CreatedAt { get; set; }

        public StoreInstalledNodeEvent(Guid nodeId)
        {
            NodeId = nodeId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}